using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private CustomNetwork networkManager;

    [SerializeField] private GameObject levelsParent;
    [SerializeField] private List<GameObject> levelsList = new List<GameObject>();
    [SerializeField] private int currentLevel;

	// Use this for initialization
	void Start () {
        currentLevel = 0;
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

    public void BuildLevelSelect()
    {
        //cycle through the children and add them tot he level list
        for (int i = 0; i < levelsParent.transform.childCount; i++)
        {
            //actually adding them
            levelsList.Add(levelsParent.transform.GetChild(i).gameObject);
        }
    }

    public void ChangeLevelSelect(int changeAmount)
    {
        //turn off current level
        levelsList[currentLevel].SetActive(false);

        //turn on next one
        currentLevel = (currentLevel + changeAmount) % (levelsList.Count - 1); //wrapping index so we dont try to acess and index that doesnt exist
        levelsList[currentLevel].SetActive(true);
    }

    public void GotoScene(string level)
    {
        networkManager.ServerChangeScene(level);
    }

    /// <summary>
    /// leave this fuckin game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }



}
