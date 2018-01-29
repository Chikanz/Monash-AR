using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;

public class PlaceOnPlane : ARBase
{
    [SerializeField]
    private GameObject m_ObjectToPlace;

    public Vector3 offset;

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camera = GetCamera();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            TryPlaceOnPlane(m_ObjectToPlace, ray, offset);
        }
    }

    public GameObject TryPlaceOnPlane(GameObject g, Ray r, Vector3 displacement)
    {
        Debug.Assert(g);

        int layerMask = 1 << LayerMask.NameToLayer("ARGameObject"); // Planes are in layer ARGameObject

        RaycastHit rayHit;
        if (Physics.Raycast(r, out rayHit, float.MaxValue, layerMask))
        {
            return Instantiate(g, rayHit.point + displacement, g.transform.rotation);
        }
        return null;
    }
}
