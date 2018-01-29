using System.Collections;
using System.Collections.Generic;
using UnityARInterface;
using UnityEngine;
using UnityEngine.UI;

public class AdjustObj : ARBase
{
    public float resolution;

    public Transform AdjustmentRoot;

    [HideInInspector]
    public GameObject ObjToAdjust;

    // Use this for initialization
    void Start ()
    {
        //Add button actions programatically
        for (int i = 0; i < AdjustmentRoot.childCount; i++)
        {
            var c = AdjustmentRoot.GetChild(i).GetComponentsInChildren<Button>();
            for (int j = 0; j < c.Length; j++)
            {
                var a = i == 0;
                var b = j == 0 ? -resolution : resolution;

                c[j].onClick.AddListener(
                    delegate
                    {
                        Adjust(a,b);
                    });
            }
        }
	}

    void Adjust(bool isX, float amnt)
    {
        //Global
        Vector3 vG = 
           new Vector3(isX ? amnt : 0,
           0,
           isX ? 0 : amnt);

        //Local, Based on camera position
        Vector3 vL = GetCamera().transform.TransformDirection(vG);
        vL.y = 0;

        ObjToAdjust.transform.position += vL;
            
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
