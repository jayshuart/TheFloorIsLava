using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability_LowGravity : ThrowParent {

    // Use this for initialization
    void Start () {
        base.Start();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

    void OnEnable()
    {
        InitializeUI("Low Gravity UI");
        base.OnEnable();
    }

    // Update is called once per frame
    void Update () {
        base.Update();

    }
}