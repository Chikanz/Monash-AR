using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;

public class PacmanRoute : Route {
    
    public float spaceBetweenPellets = 1;
    public float MouthSize;
    public GameObject EatParticles;    
    public GameObject pelletPrefab;

    private GameObject mouth;

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
        //Spawn mouth       
        var mouth = GetCamera().gameObject.AddComponent<Mouth>();
        mouth.radius = MouthSize;
        mouth.EatParticles = EatParticles;

        for (int i = 0; i < Route3d.Count; i++)
        {
            //Spawn turning point
            var tp = Instantiate(pelletPrefab, Route3d[i], Quaternion.identity, root);
            tp.transform.localScale *= 2; //Make turns bigger

            //Create pelletes between points
            bool alternate = false;
            if (i < Route3d.Count - 1)
            {
                var heading = Route3d[i + 1] - Route3d[i];
                Vector3 current = Route3d[i];
                while (Vector3.Distance(current, Route3d[i + 1]) > spaceBetweenPellets)
                {
                    current += (heading.normalized * spaceBetweenPellets); //Advance current
                    var g = Instantiate(pelletPrefab, current, Quaternion.identity, root); //mek pellet
                    g.GetComponent<pelletFloat>().alternate = alternate; //alternate float
                    alternate = !alternate;
                }
            }
        }
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        Destroy(mouth);
    }

    /// <summary>
    /// For correcting route height, kinda useless though since GPS doesn't work well indoors :/
    /// </summary>
    /// <param name="dist"></param>
    public void MoveRoot(float dist)
    {
        root.transform.position += new Vector3(0, dist, 0);
    }
}
