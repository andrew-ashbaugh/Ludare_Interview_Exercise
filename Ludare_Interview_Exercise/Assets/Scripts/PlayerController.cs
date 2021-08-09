using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //-------- Player Variables------------

    [Header("Player Variables")]
    [SerializeField]
    private float speed; // speed at which the player moves

    [SerializeField]
    private int jumpHeight; // force which is applied to player

    [SerializeField]
    private int numberOfJumps; // force which is applied to player

    [SerializeField]
    private LayerMask groundLayer; // layer which the player can jump off of

    [SerializeField]
    private float groundCheckDist;

    [SerializeField]
    private float jumpDuration; // how long the held jump lasts for

    [SerializeField]
    private GameObject sprite; // players sprite

    [SerializeField]
    private GameObject playerDeathEffect; // player death effect

    [SerializeField]
    private float deathScreenTime;

    [SerializeField]
    private GameObject playerLandPartsPrefab;

    // --------Back End Variables------------

    [Header("Back End Variables")]

    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private bool screenShake;

    [SerializeField]
    private Animator cineVCamAnimator;

    [SerializeField]
    private AudioSource jumpSfx;

    public Transform raycastPoint1; // An empty Gameobject used to track where the raycast for jumping ends
    public Transform raycastPoint2; // An empty Gameobject used to track where the raycast for jumping ends

    public bool isHit; // other objects can call this to triggger death

    private Rigidbody2D rb; // Players rigidbody
    private bool grounded1; // first ray
    private bool grounded2; // second ray
    public bool grounded; // used to check if were on the ground
    private float x; // x input
    private float jumpTimer; // internal jump timer
    private bool isJumping; // used for hold jump
    private int numCurrentJumps; // internal jump counter
    private float deathScreenTimer;
    private GameObject spawnedDeathEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        deathScreenTimer = deathScreenTime;
    }

    // Update is called once per frame
    void Update()
    {

        
        x = Input.GetAxis("Horizontal"); // get horizontal input

      
        bool wasOnGround = grounded;    // we use this to apply the landing effects


        // Linecast for ground check
        // We use 1 on each bottom corner, because the player might be half off the ground.
        grounded1 = Physics2D.Linecast(raycastPoint1.position, new Vector2(raycastPoint1.transform.position.x, raycastPoint1.transform.position.y - groundCheckDist), groundLayer); 
        grounded2 = Physics2D.Linecast(raycastPoint1.position, new Vector2(raycastPoint2.transform.position.x, raycastPoint2.transform.position.y - groundCheckDist), groundLayer); 

       
        if(grounded1 == true || grounded2 == true) // if either corner is on the ground
        {
            grounded = true; // the player is considered on the ground
        }
        else
        {
            grounded = false; // otherwise, he is not on the ground
        }

        // Land effects
        if (wasOnGround == false && grounded == true) 
        {
            StartCoroutine(SquashStretch(1.25f, 0.5f, 0.05f));
            Instantiate(playerLandPartsPrefab,transform.position,Quaternion.identity);
        }

       

        if (grounded == true) // if on the ground
        {
            numCurrentJumps = numberOfJumps; // reset jump counter
        }

        if (Input.GetButtonDown("Fire1") && numCurrentJumps >0 && isHit == false) // add short hop
        {
            jumpTimer = jumpDuration;
            rb.velocity = Vector2.up * jumpHeight;
            isJumping = true;
           
            StartCoroutine(SquashStretch(0.75f,1.25f,0.1f)); // visual squash and stretch
            if(numCurrentJumps==1)
            {
                jumpSfx.pitch = Random.Range(1.1f, 1.15f); // varies up jump sound
                jumpSfx.Play();
            }
            else
            {
                jumpSfx.pitch = Random.Range(1.5f, 1.6f); // varies up jump sound
                jumpSfx.Play();
            }

        }

        if(isJumping == true && Input.GetButton("Fire1") && isHit == false) // hold for higher jump
        {
            if(jumpTimer>0)
            {
                rb.velocity = Vector2.up * jumpHeight;
            }
            else
            {
                isJumping = false;
            }
          
        }

        if(Input.GetButtonUp("Fire1") && isHit == false) // cancel jump
        {
            jumpTimer = 0;
            isJumping = false;
            numCurrentJumps -= 1;

        }

    }

    private void FixedUpdate()
    {
        if (isHit == false) // we do this check because we dont want him moving when he is dead
        {
            transform.position = new Vector3(transform.position.x + x * speed * Time.fixedDeltaTime, transform.position.y, transform.position.z); // move the player
        }

        if(jumpTimer>0) // jump timer allows for a held jump
        {
            jumpTimer -= Time.fixedDeltaTime; // decrease our jump timer
        }

        if(isHit == true) // if you are hit
        {
            if(spawnedDeathEffect == null) // disable collision, remove physics, and spawn a death effect.
            {
                spawnedDeathEffect = (GameObject)Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
                sprite.SetActive(false);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                ScreenShake();
            }

            deathScreenTimer -= Time.fixedDeltaTime; // start the timer before the death screen comes up

            if(deathScreenTimer<=0)
            {
                loseScreen.SetActive(true);
            }
           
        }


       
    }

    public void ScreenShake()
    {
        if(screenShake == true)
        {
            cineVCamAnimator.SetTrigger("Shake");
        }
    }

    IEnumerator SquashStretch(float x, float y, float time) // simple visual squash/stretch
    {
        Vector3 originalSize = Vector3.one; // our sprite holder size
        Vector3 newSize = new Vector3(x, y, originalSize.z); // our desired size
        float t = 0;

        while(t<=1.0f) // lerp to the desired size
        {
            t += Time.deltaTime/time;
            sprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }

        t = 0;

        while(t<=1.0f) // lerp back to original size
        {
            t += Time.deltaTime / time;
            sprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
       
    }

   

   
}
