using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSelectable : MonoBehaviour {

    public GameObject Content;
    private bool contentEnabled;

    private void Start()
    {
        Content.SetActive(false);
    }

    public void ToggleSelectable()
    {
        contentEnabled = !contentEnabled;
        Content.SetActive(contentEnabled);
    }
}
