using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogRoute : Route
{
    //frog vars
    private bool frogActive;
    public GameObject FrogObj;
    private GameObject Frog;

    public float frogSpeed;
    public float PushDistance;
    private int targetCounter;
    private Vector3 target;

    public float turnSpeed;

    Transform player;

    protected override void InitRoute()
    {
        player = GetCamera().transform;

        var pos = GetCamera().transform.position;
        Frog = Instantiate(FrogObj, new Vector3(pos.x, 0, pos.z) , Quaternion.identity, root);
        frogActive = true;

        target = Route3d[targetCounter];
    }

    protected override void AddCoordinates()
    {
        AddCoordinate(-37.87646, 145.04421);
        AddCoordinate(-37.87675, 145.04403);
        AddCoordinate(-37.87667, 145.04334);
        AddCoordinate(-37.87673, 145.04329);
        AddCoordinate(-37.87676, 145.04320);
        AddCoordinate(-37.87692, 145.04304);
        AddCoordinate(-37.87691, 145.04284);
        AddCoordinate(-37.87704, 145.04271);
        AddCoordinate(-37.87695, 145.04256);
    }

    // Update is called once per frame
    void Update()
    {
        if (!frogActive) return;

        //Hop away from player
        if (Vector3.Distance(player.position, Frog.transform.position) < PushDistance)
        {
            //Move
            var dir = target - Frog.transform.position;
            Frog.transform.position += dir.normalized * frogSpeed * Time.deltaTime;

            //Face target
            Frog.transform.rotation = Quaternion.Lerp(
                Frog.transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime);

            //Check at target
            if(Vector3.Distance(Frog.transform.position, target) < 0.1f)
            {
                if(targetCounter < Route3d.Count)
                {
                    targetCounter++;
                    target = Route3d[targetCounter];
                }
            }
        }
        else
        {
            var dir = player.position - Frog.transform.position;

            //Face player
            Frog.transform.rotation = Quaternion.Lerp(
                Frog.transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime * turnSpeed);
        }
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        frogActive = false;
    }
}
