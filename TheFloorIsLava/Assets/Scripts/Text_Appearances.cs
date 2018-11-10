using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Appearances : MonoBehaviour
{

    //
    public GameObject[] texts;
    public int count = 0;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        texts = GameObject.FindGameObjectsWithTag("Text");
        //player = GameObject.Find("PH_Char2(Clone)");
        for (int i = 0; i < texts.Length; i++)
        {
            if (i == 0) i++;
            texts[i].GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (count == 0 && player.transform.position.x < 5 && player.transform.position.x > 4.5 && player.transform.position.z < 3 && player.transform.position.z > 2)
        {
            texts[count].GetComponent<Renderer>().enabled = false;
            texts[count + 1].GetComponent<Renderer>().enabled = true;
            count++;
        }
        if (count == 1 && player.transform.position.x < 2 && player.transform.position.x > 1 && player.transform.position.z < 5 && player.transform.position.z > 4)
        {
            texts[count].GetComponent<Renderer>().enabled = false;
            texts[count + 1].GetComponent<Renderer>().enabled = true;
            count++;
        }
        if (count == 2 && player.transform.position.x < -5 && player.transform.position.x > -6 && player.transform.position.z < 5 && player.transform.position.z > 4)
        {
            texts[count].GetComponent<Renderer>().enabled = false;
            texts[count + 1].GetComponent<Renderer>().enabled = true;
            count++;
        }
        if (count == 3 && player.transform.position.x < -3 && player.transform.position.x > -4 && player.transform.position.z < 5 && player.transform.position.z > 4)
        {
            texts[count].GetComponent<Renderer>().enabled = false;
            texts[count + 1].GetComponent<Renderer>().enabled = true;
            count++;
        }
        if (count == 4 && player.transform.position.x < -8 && player.transform.position.x > -10 && player.transform.position.z < 6 && player.transform.position.z > 2)
        {
            texts[count].GetComponent<Renderer>().enabled = false;
            texts[count + 1].GetComponent<Renderer>().enabled = true;
            count++;
        }
        if (count == 5 && player.transform.position.x < -13 && player.transform.position.x > -15 && player.transform.position.z < 6 && player.transform.position.z > 2)
        {
            texts[count].GetComponent<Renderer>().enabled = false;
            texts[count + 1].GetComponent<Renderer>().enabled = true;
            count++;
        }
        if (count == 6 && player.transform.position.x < -18 && player.transform.position.x > -20 && player.transform.position.z < 6 && player.transform.position.z > 2)
        {
            texts[count].GetComponent<Renderer>().enabled = false;
            texts[count + 1].GetComponent<Renderer>().enabled = true;
            count++;
        }
    }
}
