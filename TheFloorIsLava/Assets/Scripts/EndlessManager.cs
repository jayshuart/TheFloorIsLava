using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour {

    [SerializeField] private float xVariation;
    [SerializeField] private float yVariation;
    [SerializeField] private float zVariation;
    [SerializeField] private float maxGap;

    [SerializeField] private int numOfPlatforms;
    [SerializeField] private List<GameObject> platforms;
    private int platformToMove = -1;

    [SerializeField] private GameObject platformPrefab;
    private Vector3 platformPos;

    [SerializeField] private float decayTime;

	// Use this for initialization
	void Start () {
        BuildInital();
        StartCoroutine(MoveNextAfterSeconds());
		
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
            //create a platrom and add it to our list
            GameObject block = GameObject.Instantiate(platformPrefab, platformPos, this.gameObject.transform.rotation, this.gameObject.transform); //make a platform at the platformPos
            platforms.Add(block);

            //move platfomr to proper spot
            MovePlatform(block);
        }

        //platformToMove = numOfPlatforms;
    }

    private void MovePlatform(GameObject platform)
    {
        //set amount of gap
        float gapLeft = maxGap;

        //give variance to xyz positions - and count down amount of gap used
        float addZ = Random.Range(0, zVariation); //only go forward
        gapLeft-= addZ;

        float addX = Mathf.Clamp(Random.Range(-xVariation, xVariation), -gapLeft, gapLeft); //left/right
        gapLeft -= Mathf.Abs(addX);

        float addY = Random.Range(-yVariation, yVariation); //up/down

        //create new platform position
        platformPos = new Vector3((platformPos.x + addX), (platformPos.y + addY), (platformPos.z + addZ));

        //move defined obj to newly defined pos
        platform.transform.position = platformPos;
    }

    IEnumerator MoveNextAfterSeconds()
    {
        //wait for the deccay time
        yield return new WaitForSeconds(decayTime);

        //change platform index
        platformToMove = ((platformToMove + 1) % platforms.Count);

        //move platform
        MovePlatform(platforms[platformToMove]);

        //recursive- start another run
        StartCoroutine(MoveNextAfterSeconds());
    }
}
