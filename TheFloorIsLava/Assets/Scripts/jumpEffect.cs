using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpEffect : MonoBehaviour {
    private SpriteRenderer effect;
	// Use this for initialization
	void Start () {
        effect = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeIn(250f));
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.LookAt(Camera.main.transform.position);
	}

    IEnumerator FadeIn(float fadeSpeed)
    {
        //shift og alpha in direction defined by bool
        float rate = 1f / (fadeSpeed * Time.deltaTime);
        while (effect.color.a < 1f)
        {
            float alpha = (effect.color.a + rate);

            if (alpha > 255)
            {
                alpha = 255;
            }

            effect.color = new Color(effect.color.r, effect.color.g, effect.color.b, alpha);
            yield return null;
        }

        StartCoroutine(FadeOut(fadeSpeed * 3));

    }

    IEnumerator FadeOut(float fadeSpeed)
    {
        //shift og alpha in direction defined by bool v
        float rate = 1f / (fadeSpeed * Time.deltaTime);
        while (effect.color.a > .01)
        {
            float alpha = (effect.color.a - rate);

            if (alpha < 0)
            {
                alpha = 0;
            }

            effect.color = new Color(effect.color.r, effect.color.g, effect.color.b, alpha);
            yield return null;
        }
            
        //actually delete the object- we dont need tons of invisible objects
        Destroy(this.gameObject);
    }
}
