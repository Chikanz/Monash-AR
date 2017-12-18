using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Mouth : MonoBehaviour {

    public float radius = 1;
    public ParticleSystem EatParticles;

    private SphereCollider collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent<SphereCollider>();
        collider.radius = radius;
        collider.isTrigger = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<pelletFloat>())
        {
            var p = Instantiate(EatParticles, other.transform.position, Quaternion.identity, null);
            Destroy(p, 3);
            Destroy(other.gameObject);
        }
    }

}
