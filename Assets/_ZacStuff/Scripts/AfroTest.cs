using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityARInterface;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;

//Test class for placing afros
public class AfroTest : ARBase
{
    public GameObject afro;
    public LayerMask maskyboy;

    public AbstractMap _Map;

    TourPoint[] points =
    {
        new TourPoint(
            new Vector2d(-37.876661, 145.043336),
            "Entrance",
            "Welcome to Monash University Caulfield!"),


        new TourPoint(
            new Vector2d(-37.87684, 145.04331),
            "Building S",
            "iunno some other shit"),


    };



    bool touched;


    private void Start()
    {
        Invoke("Place", 1);
    }

    void Place()
    {
        //Place by lat lon
       // for (int i = 0; i < points.Length; i++)
       // {
       //     var p = Conversions.GeoToWorldPosition(points[i], _Map.CenterMercator, _Map.WorldRelativeScale);
       //     CreateAfro(new Vector3((float)p.x, 0, (float)p.y));
       //
       // }
    }

    // Update is called once per frame
    void Update()
    {
        if (!touched && Input.GetMouseButton(0))
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitinfo, 1000, maskyboy))
            {
                CreateAfro(hitinfo.point);

                touched = true;
            }

        }

        //Release
        if (touched && !Input.GetMouseButton(0))
        {
            touched = false;
        }

    }


    public GameObject CreateAfro(Vector3 pos)
    {
        //work out look at rot
        var lookv = transform.position - pos;
        var rot = Quaternion.LookRotation(lookv);
        var e = rot.eulerAngles;
        rot.eulerAngles = new Vector3(0, e.y, e.z);


        //Place afro + rotate
        return Instantiate(afro, pos, rot);
    }

    public class TourPoint
    {
        public Vector2d latlon;
        public string Name;
        public string Description;

        public TourPoint(Vector2d latlong, string name, string desc)
        {
            latlon = latlong;
            Name = name;
            Description = desc;
        }
    }

}
