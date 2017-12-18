using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;

public class ZacMath : MonoBehaviour {

    public static float Map(float value, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float oldRange = (OldMax - OldMin);
        float newRange = (NewMax - NewMin);
        float newValue = (((value - OldMin) * newRange) / oldRange) + NewMin;

        return (newValue);
    }
    
    public static Vector3 PlaceByLatLong(AbstractMap m, Vector2d latlon, float height)
    {
        //Convert position
        var p = Conversions.GeoToWorldPosition(latlon, m.CenterMercator, m.WorldRelativeScale);

        return new Vector3((float)p.x, height, (float)p.y);
        
    }
}

