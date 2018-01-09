using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;
using UnityEngine.Video;

public class TriggerVideo : ARBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(GetCamera().ScreenPointToRay(Input.mousePosition), out hit))
            {
                var vid = hit.collider.GetComponentInChildren<VideoPlayer>();
                if (vid)
                {
                    if (!vid.isPlaying)
                        vid.Play();
                    else
                        vid.Stop();
                }
            }
        }
	}
}
