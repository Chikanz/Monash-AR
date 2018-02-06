using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyreEnabler : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (InstructionManager.GetInstance().GetStep() != 5) return;

        if(other.name.Contains("Tyre"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(other.transform.parent.gameObject);
            InstructionManager.GetInstance().Advance();

            GetComponent<Collider>().enabled = false; //Disable this collider
        }
    }
}
