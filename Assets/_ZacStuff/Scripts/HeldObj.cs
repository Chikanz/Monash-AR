using System.Collections;
using UnityEngine;

public class HeldObj : MonoBehaviour
{
    protected bool moving = false;
    protected bool canMove = true;
    public float pickupSpeed = 1;
    public float moveAngle = 90;
    protected Quaternion startRot;

    protected bool swingDirection; //false is down, true is up

    // Use this for initialization
    protected virtual void Start()
    {
        startRot = transform.localRotation;
    }

    public void Activate()
    {
        if (!moving && canMove)
            StartCoroutine(DownAndUp());
    }

    protected IEnumerator DownAndUp()
    {
        yield return StartCoroutine(Swing(false));
        yield return StartCoroutine(Swing(true));
        swingDirection = false;
    }

    protected IEnumerator Swing(bool direction)
    {
        moving = true;

        swingDirection = direction;

        float t = 0;
        Quaternion currentRot = transform.localRotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, -moveAngle);
        while (t < 1 && swingDirection == direction)
        {
            if(!direction)
                transform.localRotation = Quaternion.Slerp(currentRot, endRot, t);
            else
                transform.localRotation = Quaternion.Slerp(currentRot, startRot, t);

            t += Time.deltaTime * pickupSpeed;
            if (t >= 1) break;
            yield return new YieldInstruction();
        }

        moving = false;
    }
}
