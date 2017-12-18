using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;

public class PacmanRoute : ARBase {
    
    public float spaceBetweenPellets = 1;
    public float MouthSize;
    public ParticleSystem EatParticles;


    private AbstractMap map;
    public GameObject pelletPrefab;

    Vector2d[] Route =
    {
        new Vector2d(-37.8765,145.0442),
        new Vector2d(-37.8771, 145.0438),
        new Vector2d(-37.8769, 145.0436),
        new Vector2d(-37.8767, 145.0437),
        new Vector2d(-37.8767, 145.0432),
        new Vector2d(-37.8771, 145.0427),
    };

    List<Vector3> Route3d = new List<Vector3>();

    private void Start()
    {
        //Spawn mouth        
        var m = GetCamera().gameObject.AddComponent<Mouth>();
        m.radius = MouthSize;
        m.EatParticles = EatParticles;

        //Spawn path
        Invoke("Init", 0.5f);
    }

    void Init ()
    {
        //Get map
        map = GetComponent<MapAtWorldScale>();

        //Create child
        var root = Instantiate(new GameObject("Pacman Route"), transform).transform;

        //Create points in 3D space
        for (int i = 0; i < Route.Length; i++)
        {
            var pos = ZacMath.PlaceByLatLong(map, Route[i], 0);
            Route3d.Add(pos);
            var g = Instantiate(pelletPrefab, pos, Quaternion.identity, root);
            g.transform.localScale = Vector3.one * 2; //Make turns bigger
        }        

        //Create pelletes between points
        for(int i = 0; i < Route3d.Count; i++)
        {
            bool alternate = false;
            if(i < Route3d.Count - 1)
            {
                int loopCount = 0; //debug

                var heading = Route3d[i + 1] - Route3d[i];
                Vector3 current = Route3d[i];
                while(Vector3.Distance(current, Route3d[i + 1]) > spaceBetweenPellets)
                {
                    current += (heading.normalized * spaceBetweenPellets); //Advance current
                    var g = Instantiate(pelletPrefab, current, Quaternion.identity, root); //mek pellet
                    g.GetComponent<pelletFloat>().alternate = alternate; //alternate float
                    alternate = !alternate;

                    loopCount++;
                    if(loopCount > 500)
                    {
                        Debug.Log("While loop got stuck!");
                        break;
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
