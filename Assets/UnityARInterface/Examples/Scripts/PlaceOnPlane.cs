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

			int layerMask = 1 << LayerMask.NameToLayer("ARGameObject"); // Planes are in layer ARGameObject

            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, float.MaxValue, layerMask))
                Instantiate(m_ObjectToPlace, rayHit.point + offset, Quaternion.identity);
        }
    }
}
