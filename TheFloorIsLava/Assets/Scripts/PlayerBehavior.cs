using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehavior : NetworkBehaviour {

	// PUBLIC
	public bool isGrounded;								// is the player connected with the ground
	public bool reachFinish;							// has the player reached the finish line?
	public GameObject spawnPoint;						// control spawning of the character

	// PRIVATE
	public bool debugToggle;
	[SerializeField] private GameObject stateManager;					// game statemanager
	private Rigidbody charRB;							// reference to the PC's rigidbody
	private Collider charCollider;						//
	private float yaw;									// rotation about Y axis
	private Vector3 castDown;							// search for collisions downward to fix isGrounded
	private RaycastHit hit;								// RaycastHit detection
	private List<Collider> lCollisions;					// list of all objects the character will come into contact w
	[SerializeField] private float speedVar;        	// speed of the playercharacter
	[SerializeField] private float horizontalTurn;		// horizontal speed of turning the camera
    [SerializeField] private float jumpForce;			// force in which player launches upwards
	[SerializeField] private float maxDist = 1.0f;		// maximum distance for casting


    //ability switching
    [SerializeField] private List<Behaviour> abilities;
    private int activeAbility;

    //start but only for once the network player is started
    public override void OnStartLocalPlayer()
	{
		//set this local player as the player to ghost for the cam
		GameObject ghost = GameObject.FindGameObjectWithTag ("PlayerGhost");
		ghost.GetComponent<GhostCam> ().ply = this.gameObject;

		charRB = GetComponent<Rigidbody> ();
		charCollider = GetComponent<Collider> ();

		spawnPoint = GameObject.FindGameObjectWithTag ("LobbySpawn"); //set spawn point to that of the lobby - will update later
		this.transform.position = spawnPoint.transform.position;

		castDown = Vector3.down; // (0, -1, 0);

		lCollisions = new List<Collider> ();

		yaw = 0.0f;

		isGrounded = true;

		reachFinish = false;

		debugToggle = false;

		// attach this player to the StateManager
		stateManager = GameObject.FindGameObjectWithTag("StateMan");
		StateManager t = stateManager.GetComponent<StateManager> ();
		t.playerChar = this.gameObject;

        //fill abilites list
        Behaviour[] components = this.gameObject.GetComponents<Behaviour>();

        foreach (Behaviour b in components) //cycle through all components
        {
            if (b.ToString().Contains("(Ability_" ) && !b.ToString().Contains("(Ability_DoubleJump" )) //filter for only ability scripts - and not our double jump(its always active)
            {
                abilities.Add(b); //add to list of abilities
                b.enabled = false;
            }
        }

        //set active ability
        activeAbility = 0;
        abilities[activeAbility].enabled = true;
	}

    /// <summary>
    /// Cycles the active abiility.
    /// </summary>
    private void CycleAbiility()
    {
        //check the player isnt alreayd trying to throw
        if (!Input.GetMouseButton(0))
        {
            //get input and cycle through abilites
            if (Input.GetKeyDown(KeyCode.KeypadPlus) || (Input.mouseScrollDelta.y > 0)) //cycle forward
            {
                abilities[activeAbility].enabled = false; //turn off current
                activeAbility = ((activeAbility + 1) % (abilities.Count)); //set new ability
                abilities[activeAbility].enabled = true; //turn on new current
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus) || (Input.mouseScrollDelta.y < 0)) //cycle backward
            {
                abilities[activeAbility].enabled = false;
                activeAbility = Mathf.Abs((activeAbility - 1) % (abilities.Count));
                abilities[activeAbility].enabled = true;
            }
        }


    }
	
	#region PLAYER_MECHANICS
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

	/// <summary>
	/// Allow for upwards movement; gravity affected
	/// </summary>
	void PlayerJump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) {
			//Debug.Log ("Jump!");
			charRB.AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);

		}
	}

	/// <summary>
	/// Use this to teleport a character back to lobby for whatever necessary 
	/// </summary>
	public void TeleportBackToLobby() {
		if (reachFinish == true) {
			// do teleport code here
			Debug.Log("Teleportation Initialied...");
		}
	}

	#endregion PLAYER_MECHANICS

	#region COLLISION_DETECTION
	private void OnCollisionEnter(Collision col) {
		ContactPoint[] cPoints = col.contacts;

		for (int i = 0; i < cPoints.Length; i++) {
			if (Vector3.Dot(cPoints[i].normal, Vector3.up) > 0.5f) {
				if (!lCollisions.Contains(col.collider)) {
					lCollisions.Add (col.collider);
				}
				isGrounded = true;
			}
		}
	}

	private void OnCollisionStay(Collision col) {
		ContactPoint[] cPoints = col.contacts;
		bool contact = false;

		for (int i = 0; i < cPoints.Length; i++) {
			if (Vector3.Dot(cPoints[i].normal, Vector3.up) > 0.5f) {
				contact = true;
			}
		}

		if (contact) {
			isGrounded = true;
			if (!lCollisions.Contains (col.collider)) {
				lCollisions.Add (col.collider);
			}
		} else {
			if (lCollisions.Contains(col.collider)) {
				lCollisions.Remove (col.collider);
			}
			if (lCollisions.Count == 0) {
				isGrounded = false;
			}
		}
	}

	private void OnCollisionExit(Collision col) {
		if (lCollisions.Contains(col.collider)) {
			lCollisions.Remove (col.collider);
		}
		if (lCollisions.Count == 0) {
			isGrounded = false;
			lCollisions.Remove (col.collider);
		}
	}

	/// <summary>
	/// Return character to start points
	/// </summary>
	/// <param name="col">Col.</param>
	private void OnTriggerStay(Collider col)
	{
		// Colliding with lav
		if (col.name == "PH_Lava") {
			this.transform.position = spawnPoint.transform.position;
			//Debug.Log ("triggered");
		}
	}
	#endregion

	#region DEBUG
	private void DebugToggleButton() {
		if (Input.GetKeyDown(KeyCode.B)) {
			Debug.Log ("DEBUGGING: " + debugToggle);
			debugToggle = !debugToggle;
		}
	}

	void OnDrawGizmos() {
		if (isGrounded) {
			Gizmos.color = Color.green;
		} else {
			Gizmos.color = Color.red;
		}

		if (debugToggle == true) {
			Gizmos.DrawWireCube (transform.position, new Vector3(1, 1, 1));
		}
	}

	/// <summary>
	/// Players can force their own respawn
	/// </summary>
	private void PlayerForcedRespawn()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			this.transform.position = spawnPoint.transform.position;
		}
	}
	#endregion DEBUG

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
            CycleAbiility();
		DebugToggleButton ();
        }
	
}