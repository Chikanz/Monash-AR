using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Ar;
using Mapbox.Utils;
using UnityARInterface;
using Mapbox.Unity.Map;

public class InstructionManager : ARBase
{
    public Text InstructionText;

    //1
    public Vector2d platformLocation;
    public int startDistance = 10;

    //2
    public GameObject alignmentPlatformPrefab;
    public GameObject AlignmentUI;
    private PlaceOnPlane planePlacer;
    private GameObject platform;

    private SimpleAutomaticSynchronizationContextBehaviour alignment;

    int step = 0;
    string[] Instruction =
    {
        "",
        "Walk to the destination",
        "Welcome!",
        "Align the object with the platform",
    };

	// Use this for initialization
	void Start ()
    {
        alignment = GetComponent<SimpleAutomaticSynchronizationContextBehaviour>();
        alignment.OnAlignmentAvailable += Alignment_OnAlignmentAvailable;

        //Alignment not calced in editor
#if UNITY_EDITOR
        Invoke("Advance", 0.6f);
        Invoke("Advance", 0.6f);
#endif
    }

    private void Alignment_OnAlignmentAvailable(Alignment obj)
    {
        Advance();
    }

    // Conditions
    void Update ()
    {
        switch (step)
        {
            case 1:
                //If close to start point
                if (Vector3.Distance(GetCamera().transform.position,
                    ZacMath.PlaceByLatLong(GetComponent<AbstractMap>(), platformLocation, 0)) < startDistance)
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
                    }
                    else if (!p)
                    {
                        InstructionText.text = "Couldn't find ground plane!";
                    }
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
                GetComponent<RouteManager>().SwitchRoute(1);
                break;

            case 3:
                AlignmentUI.gameObject.SetActive(true);
                planePlacer = gameObject.AddComponent<PlaceOnPlane>();
                break;

            case 4:
                AlignmentUI.gameObject.SetActive(false);
                break;
        }

    }
}
