using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour {

    [SerializeField] private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        networkManager.StartHost();
    }

    public void JoinGame()
    {
        networkManager.StartClient();
    }
}
