using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability_DoubleJump : NetworkBehaviour {

    //fields
    private Rigidbody rb;
    [SerializeField] private int jumps;
    [SerializeField] private int maxJumps;
    [SerializeField] private float jumpForce;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float timeWaited;

    [SerializeField] private float maxJumpVelocity;

    private shadowOverlay uiOverlay;

    //properties
    public float CooldownTime
    {
        get { return cooldownTime; }
    }

    public float TimeWaited
    {
        get { return timeWaited; }
    }


	// Use this for initialization
	void Start () {
        timeWaited = 0.0f;
        jumps = maxJumps;

        rb = this.gameObject.GetComponent<Rigidbody>();
	}

    //start but only for local player junk
    public override void OnStartLocalPlayer()
    {
        uiOverlay = GameObject.Find("Double Jump UI").GetComponentInChildren<shadowOverlay>();
        uiOverlay.LocalPlayer = this.gameObject;
        uiOverlay.CooldownTime = this.cooldownTime;
        uiOverlay.TimeWaited = this.timeWaited;
    }
	
	// Update is called once per frame
	void Update () {
        DoubleJump();
        CoolDown();
	}

    private void DoubleJump() 
    {
        //exit func if we are not the local player
        if (!isLocalPlayer)
        {
            return;
        }

        //get input
        if (Input.GetButtonDown("Jump") && jumps > 0 && !this.gameObject.GetComponent<PlayerBehavior>().isGrounded) //check for the jump btn, we have jumps, and that the player has a;ready exhausted their normal jump 
        {
            //apply force upwards
            Vector3 jumpVector = new Vector3(0, jumpForce, 0); 
			rb.AddForce(jumpVector, ForceMode.Impulse);

            if (rb.velocity.y > maxJumpVelocity)
            {
                rb.velocity = new Vector3(rb.velocity.x, maxJumpVelocity, rb.velocity.z);
            }

            //remove one jump
            jumps--;

            //reset cooldown
            timeWaited = 0.0f;
        }

    }

    private void CoolDown()
    {
        //update HUD info
        uiOverlay.CooldownTime = this.cooldownTime;
        uiOverlay.TimeWaited = this.timeWaited;

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
                //timeWaited = 0.0f;
            }
        }
    }
}
