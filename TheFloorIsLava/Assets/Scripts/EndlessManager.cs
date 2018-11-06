using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour {

    [SerializeField] private float gapDistance;
    [SerializeField] private int numOfPlatforms;
    [SerializeField] private GameObject platformPrefab;
    private List<GameObject> platforms;

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
        Vector3 platformPos = this.gameObject.transform.position; //start from root
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GameObject.Instantiate(platformPrefab, platformPos, this.gameObject.transform.rotation, this.gameObject.transform); //make a platform at the platformPos

            //update platform pos based on the gap distance
            /*
             *  mag = sqrRoot((x+add)^2 + y^2 + (z+add)^2)
             *  mag^2 = (x+add)^2 + y^2 + (z+add)^2
             *  sqrRoot(mag) = sqrRoot((x+add)^2 + y^2 + (z+add)^2)
             *  mag = (x+add) + y + (z + add)
             *  mag - y = x + add + z + add
             *  mag -x - y = add * 2
             *  mag / 2 = add
              */
            float addedDist = (Mathf.Sqrt((gapDistance*gapDistance) - (platformPos.x*platformPos.x) - (platformPos.y*platformPos.y) - (platformPos.z*platformPos.z)) / numOfPlatforms);

            float addX = Random.Range(0, addedDist);
            float addZ = addedDist - addX;
            platformPos = new Vector3((platformPos.x + addX), platformPos.y, (platformPos.z + addZ));
            //gapDistance = Mathf.Sqrt((platformPos.x + addedDist) ^ 2 + platformPos.y ^ 2 + (platformPos.z + addedDist ^ 2));
        }
    }
}
