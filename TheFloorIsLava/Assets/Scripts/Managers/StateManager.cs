using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

	// PUBLIC
	public GameObject playerChar;					// the character
	public GameObject lavaObj;						// lava object. May need to be an array in the future
	public GameObject lobbyResPoint;				// point to throw character back into lobby
    [SerializeField] private Lobby lobbyScript;
	public Text timeScore;							// time taken to complete level for each player
	public Text totalScore;							// final time

	// PRIVATE
	private Rigidbody charRig;						// character's rigidbody
	private GameObject finishLine;					// win state
	private float elapsedTime;						// total time since start of game

	// Use this for initialization
	void Start () {
		Cursor.visible = false;

		finishLine = GameObject.FindGameObjectWithTag ("Finisher"); // finish line instantiation

		elapsedTime = 0;
	}

	void Awake() {
		if (playerChar != null) {
			charRig = playerChar.GetComponent<Rigidbody> ();
		}
	}

	void ReachFinishLine(GameObject other) {
		if (playerChar == null) {
			return;
		}
		Collider tempCollider = other.GetComponent<Collider> ();
		//Collider tempCollider2 = playerChar.GetComponent<Collider> ();

		if (other.tag == "Finisher" && playerChar.GetComponent<Collider>().bounds.Intersects (tempCollider.bounds)) {
			// display the time taken to reach finish
            totalScore.text = elapsedTime.ToString("0.00") + " Secs";
            totalScore.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            timeScore.gameObject.SetActive(false);

			playerChar.GetComponent<PlayerBehavior>().reachFinish = true;
			playerChar.GetComponent<PlayerBehavior> ().TeleportBackToLobby ();
            lobbyScript.nextLevel = true;

		} else {
			//Debug.Log ("Unknown error/exception");
		}
		//Debug.Log (elapsedTime.ToString("0.000"));
	}


	// Update is called once per frame
	void Update () {
        //quick check for if we even should be updating time at all
        if (!lobbyScript.nextLevel)
        {
            return;
        }

        //all is well- calc time and update ui
		elapsedTime += Time.deltaTime;
		ReachFinishLine (finishLine);

		if (playerChar == null) 
			playerChar = GameObject.FindGameObjectWithTag ("Player");

		timeScore.text = elapsedTime.ToString("0.00") + " Secs";

	}
}
