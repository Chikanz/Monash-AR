using System.Collections;
using UnityEngine;

public class TyringDog : MonoBehaviour {

    bool moving = false;
    bool canMove = true;
    public float pickupSpeed = 1;
    public float moveAngle = 90;
    private Quaternion startRot;

	// Use this for initialization
	void Start ()
    {
        startRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        if(!moving && canMove)
            StartCoroutine(Swing());
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Tyre"))
        {
            //Stop moving
            canMove = false;
            StopAllCoroutines();
            moving = false;

            //Parent
            other.transform.parent.parent = transform;
            var r = other.transform.rotation;
            other.transform.rotation = Quaternion.Euler(0,r.eulerAngles.y, 0);

            //Advance Instruction
            InstructionManager.GetInstance().Advance();
        }
    }

    IEnumerator Swing()
    {
        moving = true;
        float t = 0;
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, -moveAngle);
        while (t < 1)
        {
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            t += Time.deltaTime * pickupSpeed;
            yield return new YieldInstruction();
        }
        while (t > 0)
        {
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            t -= Time.deltaTime * pickupSpeed;
            yield return new YieldInstruction();
        }
        moving = false;
    }
}
