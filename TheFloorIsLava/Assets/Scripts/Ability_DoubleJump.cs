using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Ability_DoubleJump : NetworkBehaviour {

    //fields
    private Rigidbody rb;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpForce;
    private int initalJumpForce;

    private Image uiOverlay;

	// Use this for initialization
	void Start () {
        canJump = true;

        rb = this.gameObject.GetComponent<Rigidbody>();
	}

    //start but only for local player junk
    public override void OnStartLocalPlayer()
    {
        uiOverlay = GameObject.Find("Double Jump UI").transform.GetChild(0).gameObject.GetComponentInChildren<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        DoubleJump();
	}

    private void DoubleJump() 
    {
        //exit func if we are not the local player
        if (!isLocalPlayer)
        {
            return;
        }

        //grounded check
        bool grounded = this.gameObject.GetComponent<PlayerBehavior>().isGrounded;

        if (grounded)
        {
            canJump = true;
            uiOverlay.enabled = false;
        }

        //get input
        if (Input.GetButtonDown("Jump") && canJump && !grounded) //check for the jump btn, we have jumps, and that the player has a;ready exhausted their normal jump 
        {
            //make other forces not apply to player - ie no grvity or initla jump
            rb.velocity = Vector3.zero;

            //apply force upwards
            Vector3 jumpVector = new Vector3(0, jumpForce, 0); 
            rb.AddForce(jumpVector, ForceMode.Impulse);

            //remove one jump
            canJump = false;
            uiOverlay.enabled = true;
        }

    }
}
