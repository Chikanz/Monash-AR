using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Ar;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Displays UI when not aligned
/// Switches between route methods
/// </summary>
public class RouteManager : MonoBehaviour {

    SimpleAutomaticSynchronizationContextBehaviour Alignment;
    private Route[] RouteTypes;

    public GameObject CalibrationUI;
    public GameObject RouteSwitchUI;

    public GameObject ButtonPrefab;
    public float buttonOffset;

    int currentRoute = -1;

    bool initalized = false;

    // Use this for initialization
    void Awake ()
    {
        //Debug
        enabled = false;

        Alignment = GetComponent<SimpleAutomaticSynchronizationContextBehaviour>();
        Alignment.OnAlignmentAvailable += Alignment_OnAlignmentAvailable;

        //Get route types and turn them off
        RouteTypes = GetComponents<Route>();
        foreach (Route r in RouteTypes)
        {
            r.AddCoordinate(-37.8765, 145.0442);
            r.AddCoordinate(-37.8768, 145.0440);
            r.AddCoordinate(-37.8771, 145.0438);
            r.AddCoordinate(-37.8772, 145.0442);
            r.AddCoordinate(-37.8773, 145.0443);
            r.AddCoordinate(-37.8774, 145.0449);
            r.AddCoordinate(-37.8775, 145.0449);
            r.AddCoordinate(-37.8778, 145.0448);
            r.AddCoordinate(-37.8778, 145.0449);
            r.AddCoordinate(-37.8780, 145.0462);
            r.AddCoordinate(-37.8776, 145.0463);
            r.AddCoordinate(-37.8776, 145.0461);

            r.enabled = false;
        }

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
        if (index == currentRoute) return;

        currentRoute = index;

        foreach (Route r in RouteTypes)
            r.enabled = false;

        RouteTypes[index].enabled = true;

    }

    /// <summary>
    /// Loads the wheel wright scene
    /// </summary>
    public void SkipNavigation()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Wheel Wright");
        CalibrationUI.SetActive(true);
        CalibrationUI.GetComponentInChildren<Text>().text = "Loading.....";
    }
}
