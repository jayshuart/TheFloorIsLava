using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby : NetworkBehaviour {

    [SerializeField] private CustomNetwork networkManager;
    public bool nextLevel; //check for if we are chaning level or starting level
    [SerializeField] string nextScene;

	// Use this for initialization
	void Awake () {
        networkManager = GameObject.Find("NetworkManager_Custom").GetComponent<CustomNetwork>();
        nextLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //check if this is the host
        if(col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<NetworkIdentity>().netId.Value < 0)
        {
            //you have no power here- begone!
            return;
        }
        else
        {
            if (nextLevel)
            {
                networkManager.ServerChangeScene(nextScene);
                return; //leave method
            }

            //move all players to the actual level
            foreach (GameObject ply in networkManager.Players)
            {
                //save this player bhevaior script
                PlayerBehavior plyScript = ply.GetComponent<PlayerBehavior>();

                //reset players spawnpoint
                plyScript.spawnPoint = GameObject.FindGameObjectWithTag("Respawn"); //normal spawn point

                //force respawn
                ply.transform.position = plyScript.spawnPoint.transform.position;
            }

        }


    }
}
