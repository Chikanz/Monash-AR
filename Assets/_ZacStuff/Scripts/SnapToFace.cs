using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToFace : MonoBehaviour {

    //public LayerMask mask = ~0;

    // Use this for initialization
    void Start ()
    {
        //Invoke("Init", 1);
        StartCoroutine(Snap());
	}

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Snap()
    {
        yield return new WaitForSeconds(1);

        List<RaycastHit> buildings = new List<RaycastHit>();
        var closestDist = 999.0f;
        var closestIndex = -1;

        for (int i = 0; i < 8; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, 30,
                 1 << 13, QueryTriggerInteraction.Ignore))
            {
                buildings.Add(hit);
                var dist = Vector3.Distance(transform.position, hit.point);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestIndex = buildings.Count - 1;
                }
            }
            transform.rotation *= Quaternion.Euler(new Vector3(0, 45, 0));
            //yield return new WaitForSeconds(1);
            yield return new WaitForFixedUpdate();
        }

        var closestHit = buildings[closestIndex];

        transform.position = closestHit.point - ((transform.forward * GetComponent<BoxCollider>().size.z)/2);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, closestHit.normal);
               
        
    }
}
