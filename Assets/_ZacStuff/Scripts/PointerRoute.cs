using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerRoute : Route
{
    public GameObject PointerLeft;
    public GameObject PointerRight;

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

    protected override void InitRoute()
    {
        for (int i = 0; i < Route3d.Count; i++)
        {
            if (i < Route3d.Count - 1)
            {
                GameObject g;
                var dir = Route3d[i + 1] - Route3d[i];

                if (Vector3.Dot(dir.normalized, Vector3.forward) > 0)
                {
                    g = Instantiate(PointerLeft, Route3d[i], XLookRotation(dir) * Quaternion.Euler(0, 180, 0));
                }
                else
                {
                    g = Instantiate(PointerRight, Route3d[i], XLookRotation(dir));
                }

                Objects.Add(g);
            }
        }
    }

    Quaternion XLookRotation(Vector3 right)
    {
        Vector3 up = Vector3.up;

        Quaternion rightToForward = Quaternion.Euler(0f, -90f, 0f);
        Quaternion forwardToTarget = Quaternion.LookRotation(right, up);

        return forwardToTarget * rightToForward;
    }

    protected override GameObject SpawnEndMarker()
    {
        var g = base.SpawnEndMarker();
        var dir = Route3d[Route3d.Count - 2] - Route3d[Route3d.Count - 1]; //Face towards second last point
        g.transform.rotation = Quaternion.LookRotation(dir);
        return g;
    }

}