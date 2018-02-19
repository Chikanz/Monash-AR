using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRoute : Route
{
    LineRenderer LR;

    public Material LineMat;

    //protected override void AddCoordinates()
    //{
    //    AddCoordinate(-37.87646, 145.04421);
    //    AddCoordinate(-37.87675, 145.04403);
    //    AddCoordinate(-37.87667, 145.04334);
    //    AddCoordinate(-37.87673, 145.04329);
    //    AddCoordinate(-37.87676, 145.04320);
    //    AddCoordinate(-37.87692, 145.04304);
    //    AddCoordinate(-37.87691, 145.04284);
    //    AddCoordinate(-37.87704, 145.04271);
    //    AddCoordinate(-37.87695, 145.04256);
    //}

    protected override void InitRoute()
    {
        //Create line renderer object
        var l = new GameObject("Line Renderer");
        Objects.Add(l);
        l.transform.Rotate(new Vector3(90, 0, 0));

        l.AddComponent<LineRenderer>();
        l.transform.parent = transform;

        //Set settings
        LR = l.GetComponent<LineRenderer>();
        LR.material = LineMat;
        LR.alignment = LineAlignment.Local;
        LR.positionCount = Route3d.Count;

        //Input positions
        for (int i = 0; i < Route3d.Count; i++)
        {
            LR.SetPosition(i, Route3d[i]);
        }
    }
}
