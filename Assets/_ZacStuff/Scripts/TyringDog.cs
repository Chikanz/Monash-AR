using System.Collections;
using UnityEngine;

public class TyringDog : MonoBehaviour {

    protected bool moving = false;
    bool canMove = true;
    public float pickupSpeed = 1;
    public float moveAngle = 90;
    private Quaternion startRot;

    protected bool swingDirection; //false is down, true is up

	// Use this for initialization
	protected virtual void Start ()
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

    protected virtual void OnTriggerEnter(Collider other)
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
        while (!swingDirection)
        {
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            t += Time.deltaTime * pickupSpeed;
            if (t > 1) swingDirection = true;
            yield return new YieldInstruction();
        }
        while (swingDirection & t > 0)
        {
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            t -= Time.deltaTime * pickupSpeed;
            yield return new YieldInstruction();
        }
        moving = false;
        swingDirection = false;
    }
}
