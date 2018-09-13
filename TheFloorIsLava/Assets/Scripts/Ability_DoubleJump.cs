﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability_DoubleJump : NetworkBehaviour {

    //fields
    [SerializeField] private int jumps;
    [SerializeField] private int maxJumps;
    [SerializeField] private float jumpForce;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float timeWaited;

	// Use this for initialization
	void Start () {
        timeWaited = 0.0f;
        jumps = maxJumps;
	}
	
	// Update is called once per frame
	void Update () {
        DoubleJump();
        CoolDown();
	}

    private void DoubleJump() 
    {
        //get input
        if (Input.GetButtonDown("Jump") && jumps > 0) //whatever the jump button is in the unity input editor
        {
            //apply force upwards
            Vector3 jumpVector = new Vector3(0, jumpForce, 0); 
            this.gameObject.GetComponent<Rigidbody>().AddForce(jumpVector);

            //remove one jump
            jumps--;

            //reset cooldown
            timeWaited = 0.0f;
        }

    }

    private void CoolDown()
    {
        //check if we need to even do a cooldown (maybe they have all their jumps?)
        if (jumps < maxJumps)
        {
            //up time waited
            timeWaited += Time.deltaTime;

            //check if we have waited long enough to use double jump
            if (timeWaited >= cooldownTime)
            {
                //give back a jump
                jumps++;

                //reset time waited
                timeWaited = 0.0f;
            }
        }
    }
}
