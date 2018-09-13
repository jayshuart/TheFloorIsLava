using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehavior : MonoBehaviour {

    public Vector3 Position;        // global position of pc
    public Vector3 Velocity;        // global movement speed of pc\
    public Vector3 Heading;         // the rotation/direction the pc is facing
    public float speedVar;          // 
    public float turnRate;          //

	// Use this for initialization
	void Start ()
    {
        Position = transform.position;
        Velocity = new Vector3(0, 0, 0);
        speedVar = 5.0f;
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

    /// <summary>
    /// Allow for upwards movement; gravity affected
    /// </summary>
    void PlayerJump()
    {

    }

	// Update is called once per frame
	void Update ()
    {
        PlayerMovement();
	}
}
