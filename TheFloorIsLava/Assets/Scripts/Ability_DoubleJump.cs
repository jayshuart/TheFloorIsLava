using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Ability_DoubleJump : NetworkBehaviour {

    //fields
    private Rigidbody rb;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpForce;
    private int initalJumpForce;

    private Image uiOverlay;

    [SerializeField] private GameObject jumpEffectPrefab;

	// Use this for initialization
	void Start () {
        canJump = true;

        rb = this.gameObject.GetComponent<Rigidbody>();
	}

    //start but only for local player junk
    public override void OnStartLocalPlayer()
    {
        uiOverlay = GameObject.Find("Double Jump UI").transform.GetChild(0).gameObject.GetComponentInChildren<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        DoubleJump();
	}

    private void DoubleJump() 
    {
        //exit func if we are not the local player
        if (!isLocalPlayer)
        {
            return;
        }

        //grounded check
        bool grounded = this.gameObject.GetComponent<PlayerBehavior>().isGrounded;

        if (grounded && !canJump)
        {
            canJump = true;
            StartCoroutine(FadeIn(250f));
        }

        //get input
        if (Input.GetButtonDown("Jump") && canJump && !grounded) //check for the jump btn, we have jumps, and that the player has a;ready exhausted their normal jump 
        {
            //make a cool smoke effect
            GameObject.Instantiate(jumpEffectPrefab, this.gameObject.transform.position, this.transform.rotation);

            //make other forces not apply to player - ie no grvity or initla jump
            rb.velocity = Vector3.zero;

            //apply force upwards
            Vector3 jumpVector = new Vector3(0, jumpForce, 0); 
            rb.AddForce(jumpVector, ForceMode.Impulse);

            //remove one jump
            canJump = false;
            //uiOverlay.enabled = true;
            StartCoroutine(FadeOut(300f));
        }
    }

    IEnumerator FadeOut(float fadeSpeed)
    {
        //shift og alpha in direction defined by bool
        float rate = 255 / (fadeSpeed * Time.deltaTime);
        while (uiOverlay.color.a < 254)
        {
            float alpha = (uiOverlay.color.a + rate);

            if (alpha > 255)
            {
                alpha = 255;
            }

            uiOverlay.color = new Color(uiOverlay.color.r, uiOverlay.color.g, uiOverlay.color.b, alpha);
            yield return null;
        }
    }

    IEnumerator FadeIn(float fadeSpeed)
    {
        //shift og alpha in direction defined by bool
        float rate = 255 / (fadeSpeed * Time.deltaTime);
        while (uiOverlay.color.a > 1)
        {
            float alpha = (uiOverlay.color.a - rate);

            if (alpha < 0)
            {
                alpha = 0;
            }

            uiOverlay.color = new Color(uiOverlay.color.r, uiOverlay.color.g, uiOverlay.color.b, alpha);
            yield return null;
        }
    }
}
