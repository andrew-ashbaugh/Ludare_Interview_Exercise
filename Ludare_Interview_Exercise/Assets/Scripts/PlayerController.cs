using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float jumpDuration; // how long the held jump lasts for

    [SerializeField]
    private GameObject sprite; // players sprite

    [SerializeField]
    private GameObject playerDeathEffect;

    [SerializeField]
    private float respawnDuration;

   
    
    // --------Back End Variables------------

    [Header("Back End Variables")]
    public Transform raycastPoint1; // An empty Gameobject used to track where the raycast for jumping ends
    public Transform raycastPoint2; // An empty Gameobject used to track where the raycast for jumping ends
    private Rigidbody2D rb; // Players rigidbody
    private bool grounded1; // first ray
    private bool grounded2; // second ray
    public bool grounded; // used to check if were on the ground
    private float x; // x input
    private float jumpTimer; // internal jump timer
    private bool isJumping; // used for hold jump
    private int numCurrentJumps; // internal jump counter
    public bool isHit; // other objects can call this to triggger death
    private float respawnTimer;
    private GameObject spawnedDeathEffect;
    [SerializeField]
    private GameObject loseScreen;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        respawnTimer = respawnDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // Linecast for ground check
        grounded1 = Physics2D.Linecast(transform.position, raycastPoint1.position, groundLayer); // We use 1 on each bottom corner, because the player might be half off the ground.
        grounded2 = Physics2D.Linecast(transform.position, raycastPoint1.position, groundLayer); 

        if(grounded1 == true || grounded2 == true) // if either corner is on the ground
        {
            grounded = true; // the player is considered on the ground
        }
        else
        {
            grounded = false; // otherwise, he is not on the ground
        }

        x = Input.GetAxis("Horizontal"); // get horizontal input

        if (grounded == true) // if on the ground
        {
           // isJumping = false;
            numCurrentJumps = numberOfJumps; // reset jump counter
        }

        if (Input.GetButtonDown("Fire1") && numCurrentJumps >0 && isHit == false) // add short hop
        {
            jumpTimer = jumpDuration;
            //rb.velocity = Vector3.zero;
            rb.velocity = Vector2.up * jumpHeight;
            isJumping = true;
            numCurrentJumps -= 1;
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
            
        }

    }

    private void FixedUpdate()
    {
        if (isHit == false) // we do this check because we dont want him moving when he is dead
        {
            transform.position = new Vector3(transform.position.x + x * speed * Time.deltaTime, transform.position.y, transform.position.z); // move the player
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
               
            }

            respawnTimer -= Time.fixedDeltaTime; // start a respawn timer

            if(respawnTimer<=0)
            {
                loseScreen.SetActive(true);
            }
           
        }


       
    }

   

   
}
