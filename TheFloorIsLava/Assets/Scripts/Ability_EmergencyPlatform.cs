using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Ability_EmergencyPlatform : ThrowParent {

    // Use this for initialization
    void Start () {
        base.Start();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //InitializeUI("Emergency Platform UI");
    }

    void OnEnable()
    {
        InitializeUI("Emergency Platform UI");
        base.OnEnable();
    }


    // Update is called once per frame
    void Update () {
        base.Update();

    }
}
   