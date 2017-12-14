using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZacMath : MonoBehaviour {

    public static float Map(float value, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float oldRange = (OldMax - OldMin);
        float newRange = (NewMax - NewMin);
        float newValue = (((value - OldMin) * newRange) / oldRange) + NewMin;

        return (newValue);
    }
}
