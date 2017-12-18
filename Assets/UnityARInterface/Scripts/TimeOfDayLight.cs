using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class TimeOfDayLight : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {        

        var n = calcJulianDate(DateTime.Now) - 2451545.0;
        var L = 280.460 + 0.9856474 * n;
        var g = 357.528 + 0.9856474 * n;

        LogVals(L, g);

        L = PutInRange(L, 0, 360, 360);
        g = PutInRange(g, 0, 360, 360);

        LogVals(L, g);

        var elipticLongitude = L + 1.915 * Mathf.Sin((float)g) + 0.020 * Mathf.Sin((float)(2 * g));

        Debug.Log("EL: " + elipticLongitude);

    }

    void LogVals(params double[] values)
    {
        foreach(double v in values)
        {
            Debug.Log(v);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static double calcJulianDate(System.DateTime date)
    {
        return date.ToOADate() + 2415018.5;
    }

    public double PutInRange(double num, int rangeMin, int rangeMax, int multiple)
    {
        var result = num;
        while (result < rangeMin || result > rangeMax) //While not in range
        {
            if(result > rangeMax) //Bigger than range
            {
                result -= multiple;
            }
            else if(result < rangeMin)//less than range
            {
                result += multiple;
            }
        }
        return result;
    }
}
