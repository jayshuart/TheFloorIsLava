using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject levelBtnPrefab;
    private List<string> sceneList = new List<string>();

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

    public void BuildLevelSelect()
    {
        foreach(EditorBuildSettingsScene level in EditorBuildSettings.scenes)
        {
            //get enabled levels that are int he levels folder
            if (level.enabled && level.path.ToString().Contains("Assets/Scenes/Levels/"))
            {
                //add scene to actual list
                sceneList.Add(level.path);

                //create btn for this level
                GameObject lvlBtn = Instantiate(levelBtnPrefab, this.gameObject.transform);

                //rip just the levels name from the unity scene file
                string lvlName = level.path.ToString().Substring(21, (level.path.ToString().Substring(21).Length - 6));

                //apply this name to the buttons text
                lvlBtn.GetComponentInChildren<Text>().text = lvlName;
            }
        }

    }
}
