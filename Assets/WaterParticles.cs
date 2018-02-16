using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticles : MonoBehaviour {

    public float ColChangeRate = 0.001f;
    public Color TyreEndCol;

    private Color _tyreStartCol = Color.black;
    private MeshRenderer _tyreRenderer;

    private float TyreLerpCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Tyre"))
        {
            //Cache Mesh render
            _tyreRenderer = other.GetComponent<MeshRenderer>();

            //Set Start col
            if (_tyreStartCol == Color.black)
                _tyreStartCol = _tyreRenderer.material.color;

            if (TyreLerpCount < 1)
                TyreLerpCount += ColChangeRate;

            other.transform.GetChild(1).gameObject.SetActive(true);
           

            _tyreRenderer.material.color = Color.Lerp(_tyreStartCol, TyreEndCol, TyreLerpCount);
        }
    }
}
