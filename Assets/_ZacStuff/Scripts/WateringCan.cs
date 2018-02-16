using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : HeldObj
{
    private ParticleSystem _myParticles;
    public bool HoldToActivate = false;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _myParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void ActivateToggle(bool b)
    {
        if (swingDirection == !b || moving) return;
        StartCoroutine(Swing(!b));

        if (!swingDirection)
            _myParticles.Play();
        else
            _myParticles.Stop();
    }

    // Update is called once per frame
    void Update ()
    {
       
	}
}
