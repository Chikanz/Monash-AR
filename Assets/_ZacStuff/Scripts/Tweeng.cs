using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static IEnumerator Tweeng(this float duration, System.Action<float> vary, float aa, float zz)
    {
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT)
        {
            float t = (Time.time - sT) / duration;
            vary(Mathf.SmoothStep(aa, zz, t)); // slight difference here
            yield return null;
        }

        vary(zz);
    }
}
