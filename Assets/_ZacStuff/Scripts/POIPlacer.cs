using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class POIPlacer : MonoBehaviour
{
    public List<POI> Points = new List<POI>();

	// Use this for initialization
	void Start ()
    {
        Invoke("Init", 1);
	}

    private void Init()
    {
        foreach(POI p in Points)
        {
            var g = Instantiate(p.Object,
                ZacMath.PlaceByLatLong(GetComponentInParent<AbstractMap>(), p.Coords, p.Height),
                Quaternion.identity, transform);

            if (p.SnapToNearestBuilding)
                g.AddComponent<SnapToFace>();

            if (p.setRot)
                g.transform.rotation = Quaternion.Euler(p.RotToSet);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}


[System.Serializable]
public class POI
{
    public Vector2d Coords;
    public GameObject Object;
    public bool SnapToNearestBuilding;
    public float Height;
    public bool setRot;
    public Vector3 RotToSet;
}

