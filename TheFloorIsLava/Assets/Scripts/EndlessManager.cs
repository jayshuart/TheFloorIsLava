using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour {

    [SerializeField] private float xVariation;
    [SerializeField] private float yVariation;
    [SerializeField] private float zVariation;
    [SerializeField] private float maxGap;
    [SerializeField] private int numOfPlatforms;
    [SerializeField] private GameObject platformPrefab;
    private List<GameObject> platforms;
    Vector3 platformPos;

	// Use this for initialization
	void Start () {
        BuildInital();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BuildInital()
    {
        //build platforms in loop
        platformPos = this.gameObject.transform.position; //start from root
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GameObject block = GeneratePlatform();

            //update decaytime so new blocks generate not in blocks
            block.GetComponent<EndlessPlatform>().decayTime += i * 2;
        }
    }

    public GameObject GeneratePlatform()
    {
        float gapLeft = maxGap;

        float addZ = Random.Range(0, zVariation); //only go forward
        gapLeft-= addZ;

        float addX = Mathf.Clamp(Random.Range(-xVariation, xVariation), -gapLeft, gapLeft); //left/right
        gapLeft -= Mathf.Abs(addX);


        float addY = Random.Range(-yVariation, yVariation); //up/down

        platformPos = new Vector3((platformPos.x + addX), (platformPos.y + addY), (platformPos.z + addZ));
        GameObject block = GameObject.Instantiate(platformPrefab, platformPos, this.gameObject.transform.rotation, this.gameObject.transform); //make a platform at the platformPos

        return block;
    }
}
