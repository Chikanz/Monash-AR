using System.Collections;
using UnityEngine;

public class TyringDog : HeldObj
{
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
}
