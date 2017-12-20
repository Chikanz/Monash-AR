using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Ar;
using UnityEngine.UI;

/// <summary>
/// Displays UI when not aligned
/// Switches between route methods
/// </summary>
[RequireComponent(typeof(SimpleAutomaticSynchronizationContextBehaviour))]
public class RouteManager : MonoBehaviour {

    SimpleAutomaticSynchronizationContextBehaviour Alignment;
    private Route[] RouteTypes;

    public GameObject CalibrationUI;
    public GameObject RouteSwitchUI;

    public GameObject loadingDancer;
    public GameObject ButtonPrefab;
    public float buttonOffset;

    bool initalized = false;

    // Use this for initialization
    void Awake ()
    {
        Alignment = GetComponent<SimpleAutomaticSynchronizationContextBehaviour>();
        Alignment.OnAlignmentAvailable += Alignment_OnAlignmentAvailable;

        //Get route types and turn them off
        RouteTypes = GetComponents<Route>();
        foreach (Route r in RouteTypes) r.enabled = false;

#if UNITY_EDITOR
        Invoke("InitEditorAlignment", 0.5f);
#endif
    }

    void InitEditorAlignment()
    {
        Alignment_OnAlignmentAvailable(new Alignment());
    }

    void InitUI()
    {
        CalibrationUI.SetActive(true);
        //spawn loading dancer
    }

    //Called on alignment, alignment obj is never actually used
    void Alignment_OnAlignmentAvailable(Alignment obj)
    {
        if (initalized) return; //Sanity check

        CalibrationUI.SetActive(false);
        //SwitchRoute(0); //turn on route
        initalized = true;

        //Make buttons
        for(int i = 0; i < RouteTypes.Length; i++)
        {
            var b = Instantiate(ButtonPrefab, RouteSwitchUI.transform);
            var button = b.GetComponent<Button>();
            var copy = i;
            button.onClick.AddListener(delegate { SwitchRoute(copy); }); //ooft            
            b.GetComponentInChildren<Text>().text = RouteTypes[i].RouteType;
            b.GetComponent<RectTransform>().localPosition = new Vector3(0, buttonOffset * i, 0);
        }

    }

    /// <summary>
    /// Switch between routes
    /// </summary>
    /// <param name="index"></param>
    public void SwitchRoute(int index)
    {
        foreach (Route r in RouteTypes)
            r.enabled = false;

        RouteTypes[index].enabled = true;

    }
}
