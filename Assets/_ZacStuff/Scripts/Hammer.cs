using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : HeldObj {
    
    [HideInInspector]
    public Transform tyre;
    public float hitMagnitude = 5;

	// Use this for initialization
	override protected void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void OnTriggerEnter(Collider other)
    {
        //Trigger the dumb hammerpoint object
        if (other.gameObject.tag.Equals("HammerPoint") && !swingDirection && moving)
        {
            //Hit effect
            var euler = Quaternion.Euler(0, 0, 90);
            var hitPoint = other.transform.localPosition;
            hitPoint.z = 0;
            var vec = euler * hitPoint;
            tyre.Rotate(vec * hitMagnitude);

            //Move down
            if(tyre.transform.localPosition.z > 0)
                tyre.transform.localPosition -= new Vector3(0, 0, 0.0001f);
            
            if (tyre.transform.localPosition.z < 0)
                tyre.transform.localPosition = Vector3.zero;

            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.gameObject.GetComponent<AudioSource>().Play();

            if (!swingDirection)
                swingDirection = true;   
        }
    }

    protected void OnCollisionEnter(Collision other)
    {
        
    }
}
