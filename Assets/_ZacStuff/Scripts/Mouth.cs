using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class Mouth : MonoBehaviour {

    public float radius = 1;
    public GameObject EatParticles;

    private SphereCollider _myCollider;

	// Use this for initialization
	void Start () {
        _myCollider = GetComponent<SphereCollider>();
        _myCollider.radius = radius;
        _myCollider.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var pf = other.GetComponentInParent<pelletFloat>();
        if (pf)
        {
            var p = Instantiate(EatParticles, other.transform.position, Quaternion.identity, null);
            Destroy(p, 1);
            Destroy(other.gameObject);

            if(pf.IsEndMarker) //Load new scene on collision with end marker
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Wheel Wright");
            }
        }
    }

}
