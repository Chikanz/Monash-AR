using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Ar;
using Mapbox.Utils;
using UnityARInterface;
using Mapbox.Unity.Map;

/// <summary>
/// Singleton manager for ensuring tasks go step by step
/// </summary>
public class InstructionManager : ARBase
{
    static InstructionManager instance;

    public Text InstructionText;

    //1
    public Vector2d platformLocation;
    public int startDistance = 10;

    //2
    public GameObject alignmentPlatformPrefab;
    public GameObject AlignmentUI;
    private PlaceOnPlane planePlacer;
    private GameObject platform;

    //3
    public GameObject TyringDog;
    private TyringDog TyringDogInstance;

    //6
    int chocksHit;
    public int ChockCount;

    //7
    public GameObject Hammer;
    private Hammer HammerInstance;
    private HammerPoints hammerPoints;
    private GameObject tyre;

    //8
    public GameObject WateringCanObj;
    private WateringCan wateringCanInstance;
    private Color TyreEndColor;

    //private SimpleAutomaticSynchronizationContextBehaviour alignment;

    int step = 1;
    string[] Instruction =
    {
        "",
        "Walk to the destination",
        "Welcome! Tap to begin",
        "Align the object with the platform",
        "1. Tap to remove the tyre from the fire with the tyring dog",
        "2. Put the red hot tyre on the wheel",
        "3. Remove the chocks",
        "4. Hammer the wheel into place",
        "5. Water the wheel to cool it down and tighten it on the wheel",
        "6. Congratulations! You've finished the wheel!"
    };

    public static InstructionManager GetInstance()
    {
        return instance;
    }

    public int GetStep()
    {
        return step;
    }

	// Use this for initialization
	void Start ()
    {
        //Enforce singleton
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    // Conditions
    void Update ()
    {
        switch (step)
        {
            case 1:
                Advance();
                break;

            case 2:
                //Tap to advance
                if (Input.GetMouseButton(0))
                    Advance();
                break;

            case 3:
                //Platform aligned and OK pressed (Called from onclick)
                if (!platform)
                {
                    var cam = GetCamera();
                    Ray r = new Ray(cam.transform.position, cam.transform.forward);
                    var p = planePlacer.TryPlaceOnPlane(alignmentPlatformPrefab, r, Vector3.zero);
                    if (p)
                    {
                        platform = p;
                        Destroy(planePlacer);
                        AlignmentUI.GetComponent<AdjustObj>().ObjToAdjust = platform;
                        InstructionText.text = Instruction[step];

                        //Set vars
                        hammerPoints = platform.transform.GetChild(0).GetChild(0).GetComponentInChildren<HammerPoints>();
                        tyre = platform.transform.GetChild(0).GetChild(0).gameObject;
                    }
                    else if (!p)
                    {
                        InstructionText.text = "Please look around some more!";
                    }
                }
                break;

            case 4:
                if (Input.GetMouseButtonDown(0))
                {
                    TyringDogInstance.Activate();
                }
                break;

            case 6: //Destroy chocks on hit
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = GetCamera().ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.tag.Equals("Chock"))
                        {
                            Destroy(hit.collider.gameObject);
                            chocksHit++;
                            if (chocksHit >= ChockCount) Advance();
                        }
                    }
                }
                break;

            case 7:
                if (Input.GetMouseButtonDown(0))
                {
                    HammerInstance.Activate();
                }

                Debug.Log(Vector3.Dot(Vector3.up, tyre.transform.forward));
                if (Vector3.Dot(Vector3.up, tyre.transform.forward) > 0.9999f && tyre.transform.localPosition == Vector3.zero)
                {
                    Advance();
                }
                break;

            case 8:
                wateringCanInstance.ActivateToggle(Input.GetMouseButton(0));

                if(tyre.GetComponent<MeshRenderer>().material.color.Equals(TyreEndColor))
                {
                    Advance();
                }
                break;
        }
    }
    
    public void Advance()
    {
        step++;

        InstructionText.text = Instruction[step];

        switch (step)
        {
            case 1:
                //GetComponent<RouteManager>().SwitchRoute(1);
                break;

            case 3: //Align platform
                AlignmentUI.gameObject.SetActive(true);
                planePlacer = gameObject.AddComponent<PlaceOnPlane>();
                break;

            case 4: //Pickup tyre
                AlignmentUI.gameObject.SetActive(false);

                //Spawn tyre dog
                TyringDogInstance = (TyringDog) SpawnHeldObj(TyringDog);

                //Disable platform enable wheel
                platform.transform.GetChild(1).gameObject.SetActive(false);
                platform.transform.GetChild(0).gameObject.SetActive(true);
                break;

            case 6:
                Destroy(TyringDogInstance.gameObject);
                break;

            case 7:
                hammerPoints.gameObject.SetActive(true);

                //Spawn hammer
                HammerInstance = (Hammer)SpawnHeldObj(Hammer);

                HammerInstance.tyre = tyre.transform;
                break;

            case 8:
                //Spawn can
                Destroy(HammerInstance.gameObject);
                wateringCanInstance = (WateringCan)SpawnHeldObj(WateringCanObj);
                tyre.GetComponent<BoxCollider>().enabled = true;

                //Get final color
                TyreEndColor = wateringCanInstance.GetComponentInChildren<WaterParticles>().TyreEndCol;
                break;

            case 9:
                Destroy(wateringCanInstance.gameObject);
                var p = tyre.transform.GetChild(1).GetComponent<ParticleSystem>();
                p.Stop();
                Destroy(p, 3);
                break;
        }

    }

    private HeldObj SpawnHeldObj(GameObject ObjToSpawn)
    {
        var cam = GetCamera();
        HeldObj returnObj = Instantiate(ObjToSpawn, cam.transform.position, cam.transform.rotation, cam.transform).GetComponent<HeldObj>();
        returnObj.transform.localRotation = Quaternion.Euler(0, -90, 0);
        returnObj.transform.localPosition = new Vector3(0.2f, -0.475f, 0.764f);
        return returnObj;
    }
}
