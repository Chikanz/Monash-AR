using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToFace : MonoBehaviour {

    public LayerMask mask = ~0;

    // Use this for initialization
    void Start ()
    {
        //Invoke("Init", 1);
        Init();
	}

    private void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, 10, mask, QueryTriggerInteraction.Ignore))
            {
                transform.position = hit.point + (transform.forward * GetComponent<BoxCollider>().size.z / 2);
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            }
            else
            {
                transform.Rotate(new Vector3(0, 90, 0));
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
