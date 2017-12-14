using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Picks a building from map view

[RequireComponent(typeof(Camera))]
public class PickBuilding : MonoBehaviour {

    public Camera ARCam;
    public GameObject Arrow;

    Camera _cam;

    public Toggle ARToggle;

    public LayerMask Mask;

    bool buildingPicked = false;

    private GameObject arrow;
    private GameObject building;


	// Use this for initialization
	void Start ()
    {
        _cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(_cam.enabled && Input.GetMouseButtonDown(0))
        {
            Ray r = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;            
            if (Physics.Raycast(r, out hitinfo))
            {
                //Get building
                building = hitinfo.collider.gameObject;
                building.AddComponent<FlashBuilding>();

                //Toggle on
                //_cam.enabled = false;
                ARToggle.isOn = true;

                //Make arrow
                arrow = Instantiate(Arrow, ARCam.transform.position + (ARCam.transform.forward * 7) + (ARCam.transform.up * 1), Quaternion.identity);
                arrow.transform.parent = ARCam.transform;

                //Refresh target
                var face = arrow.GetComponent<FaceTowards>();
                face.TargetRend = hitinfo.transform.GetComponent<Renderer>();
                face.refreshTarget = true;
                face.ARCam = ARCam.transform;

                buildingPicked = true;

            }
        }
	}

    //Deselect the building 
    public void Deselect()
    {
        if (!buildingPicked) return; //Return if not picked

        buildingPicked = false;
        Destroy(arrow);
        var flash = building.GetComponent<FlashBuilding>();
        if (true)
        {
            flash.Stop();
            Destroy(flash);
        }
    }
}
