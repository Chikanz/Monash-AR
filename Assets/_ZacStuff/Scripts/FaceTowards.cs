using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowards : MonoBehaviour {

    [HideInInspector]
    public Vector3 Target;
    public bool fadeOnAligned = true;
    public bool DestroyOnAlign;

    [Range(-0.0f, 1.0f)]
    public float transparency;

    [Range(-1.0f,1.0f)]
    public float thresh;

    public Transform ARCam;

    public bool refreshTarget = false;
    public Renderer TargetRend;

    public bool OnlyRotY = true;

    List<MeshRenderer> rends = new List<MeshRenderer>();

	// Use this for initialization
	void Start ()
    {
        if(fadeOnAligned)
        {
            foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>())
            {
                rends.Add(rend);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 facing = Target - transform.position;
        transform.rotation = Quaternion.LookRotation(facing);

        if(OnlyRotY)
        {
            var rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(0,rot.y,0));
        }

        if(fadeOnAligned)
        {
            var dot = Vector3.Dot(ARCam.forward.normalized, facing.normalized);
            var dotMapped = ZacMath.Map(dot, -1, 1, 0, 1);
            UpdateTransparent(Mathf.Lerp(transparency, 0, dotMapped));

            if (DestroyOnAlign && dotMapped > 0.9f)
            {
                Destroy(gameObject);
            }
        }

        if(refreshTarget)
        {
            Target = TargetRend.bounds.center;
        }

      
	}

    void UpdateTransparent(float a)
    {
        foreach(MeshRenderer r in rends)
        {
            var c = r.material.color;
            c.a = a;
            r.material.color = c;
        }
    }
}
