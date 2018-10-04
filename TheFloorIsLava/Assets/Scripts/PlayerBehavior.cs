using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehavior : NetworkBehaviour {

	// PUBLIC
	public GameObject spawnPoint;	// control spawning of the character
    [SerializeField] private float speedVar;          // speed of the pc
	[SerializeField] private float horizontalTurn;	// horizontal speed of turning the camera
	public bool isGrounded;			// is the player connected with the ground

    public Vector3 vel;

	// PRIVATE
	private Rigidbody charRB;				// reference to the PC's rigidbody
	private float yaw;				// rotation about Y axis
    [SerializeField] private float jumpForce;

    //start but only for once the network player is started
    public override void OnStartLocalPlayer()
    {
        //set this local player as the player to ghost for the cam
        GameObject ghost = GameObject.FindGameObjectWithTag("PlayerGhost");
        ghost.GetComponent<GhostCam>().ply = this.gameObject;

        spawnPoint = GameObject.FindGameObjectWithTag("Respawn"); //set spawn point
		this.transform.position = spawnPoint.transform.position;
    }

	// Use this for initialization
	void Start ()
    {
		// INSTANTIATE GLOBALS
        //speedVar = 5.0f;
		charRB = GetComponent<Rigidbody> ();
		isGrounded = false;

		// CAMERA INSTANTIATIONS
		//horizontalTurn = 6.5f;
		yaw = 0.0f;
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

	/// <summary>
	/// What allows character to turn about the Y axis
	/// </summary>
    void PlayerViewRotation()
    {
		yaw += horizontalTurn * Input.GetAxis ("Mouse X");

		charRB.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);	// Euler Angles to prevent gimbal locking (as with previous issue)
    }

	void onGround() 
	{
		if (!isGrounded && charRB.velocity.y == 0) { //might need updating using a raycast later - joel
			isGrounded = true;
		}
	}

    /// <summary>
    /// Allow for upwards movement; gravity affected
    /// </summary>
    void PlayerJump()
    {
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) {
			Debug.Log ("Jump!");
			charRB.AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
			
		}
        isGrounded = false;
    }

	/// <summary>
	/// Return character to start points
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerStay(Collider col)
	{
		if (col.name == "PH_Lava") {
			this.transform.position = spawnPoint.transform.position;
			Debug.Log ("triggered");
		}
	}

	void PlayerForcedRespawn()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			this.transform.position = spawnPoint.transform.position;
		}
	}

	// Update is called once per frame
	void Update ()
    {
        //make sure we are the local player before we get user input
        if (isLocalPlayer)
        {
            //ply movemenvt and cam
            PlayerMovement();
            PlayerViewRotation ();
            PlayerJump ();
        }

        //check if ply is gorunded so it may jump again
        onGround ();
	}
}
