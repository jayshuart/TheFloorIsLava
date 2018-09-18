﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehavior : MonoBehaviour {

    public Vector3 Position;        // global position of pc
    public Vector3 Velocity;        // global movement speed of pc\
    public Vector3 Heading;         // the rotation/direction the pc is facing
    public float speedVar;          // speed of the pc
    public float turnRate;          // speed in which pc turns
	public bool isGrounded;			// is the player connected with the ground
	Rigidbody charRB;				// reference to the PC's rigidbody

	// Use this for initialization
	void Start ()
    {
        Position = transform.position;
        Velocity = new Vector3(0, 0, 0);
        speedVar = 5.0f;
		charRB = GetComponent<Rigidbody> ();
		isGrounded = false;
	}
	
    /// <summary>
    /// Move player in X/Y axis; allow for diagonal movement
    /// </summary>
    void PlayerMovement()
    {
        // Track movements in LEFT/RIGHT and FORWARD/BACKWARD
        var xMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speedVar;
        var zMovement = Input.GetAxis("Vertical") * Time.deltaTime * speedVar;

        transform.Translate(xMovement, 0, zMovement);
    }

    void PlayerRotation()
    {

    }

	void onGround() 
	{
		if (!isGrounded & charRB.velocity.y == 0) {
			isGrounded = true;
		}
	}

    /// <summary>
    /// Allow for upwards movement; gravity affected
    /// </summary>
    void PlayerJump()
    {
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) {
			//Debug.Log ("Jump!");
			charRB.AddForce (new Vector3 (0, 4.0f, 0), ForceMode.Impulse);
			isGrounded = false;
		}
    }

	/// <summary>
	/// Initial touching of the lava
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.name == "PH_Lava") {
			Debug.Log ("hit");
		}
	}

	/// <summary>
	/// Prolonged touching of the lava
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionStay(Collision col)
	{
		if (col.collider.name == "PH_Lava") {
			Debug.Log ("hit");
		}
	}

	// Update is called once per frame
	void Update ()
    {
        PlayerMovement();
		onGround ();
		PlayerJump ();
		//Debug.Log (isGrounded);
	}
}
