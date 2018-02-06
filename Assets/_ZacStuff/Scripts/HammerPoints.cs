using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerPoints : MonoBehaviour
{
    public int HammerCount = 8;
    public float wheelRadius = 1;
    public GameObject HammerPointPrefab;
    private GameObject[] Points;


	// Use this for initialization
	void Start ()
    {
        Points = new GameObject[HammerCount];

        for (int i = 0; i < HammerCount; i++)
        {
            var angle = (360f / HammerCount);
            var euler = Quaternion.Euler(0, angle * i, 0); //Rotate the vector by angle on each iteration
            var vec = euler * Vector3.forward;
            var pos = transform.position + (wheelRadius * vec);
            Points[i] = Instantiate(HammerPointPrefab, pos, Quaternion.identity, transform);

            gameObject.SetActive(false);
        }
    }

    public bool HammeringComplete()
    {
        foreach(GameObject g in Points)
        {
            if (g.activeSelf) return false;
        }
        return true;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
