using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPlatform : MonoBehaviour {
    private EndlessManager endManager;
    [SerializeField] public float decayTime;

	// Use this for initialization
	void Start () {
        endManager = GameObject.FindWithTag("EndlessManager").GetComponent<EndlessManager>();
        StartCoroutine(WaitAndDestory());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        //create a new platform to replace this one
        endManager.GeneratePlatform(); 
    }

    IEnumerator WaitAndDestory()
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(this.gameObject);
    }
}
