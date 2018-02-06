using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : TyringDog {

    Transform wheel;
    public float hitMagnitude = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    new void OnTriggerEnter(Collider other)
    {
        //Trigger the dumb hammerpoint object
        if(other.tag.Equals("HammerPoint"))
        {
            other.GetComponent<ParticleSystem>().Play();
            other.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other, 3);

            //Hit effect
            wheel.Rotate(Random.insideUnitSphere * hitMagnitude);
        }
    }
}
