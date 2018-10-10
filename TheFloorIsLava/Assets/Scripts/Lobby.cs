using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby : NetworkBehaviour {

    private CustomNetwork networkManager;

	// Use this for initialization
	void Start () {
        networkManager = GameObject.Find("NetworkManager_Custom").GetComponent<CustomNetwork>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //check if this is the host
        if(col.gameObject.GetComponent<NetworkIdentity>().netId.Value != 1)
        {
            //you have no power here- begone!
            return;
        }
        else
        {
            
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
