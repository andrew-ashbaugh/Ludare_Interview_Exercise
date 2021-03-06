using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Used on every enemy 

    //-------- Enemy Variables------------

    [Header("Enemy Variables")]
    [SerializeField]

    private float speed; // speed of the enemy

    [SerializeField]
    private float playerBounceForce; // how high the player bounces off enemies heads

    [SerializeField]
    private GameObject explosionPrefab; // enemy death explosion

    [SerializeField]
    private GameObject bumpFxPrefab; // hit off walls fx

    //-------- Back End Variables------------

    [Header("Back End Variables")]

    [SerializeField]
    private int startingDirection; // used to make more interesting level layouts

    private float dir; //1 -> right, -1 -> left
    private Rigidbody2D rb;
    public bool dead;



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        dir = startingDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * dir, rb.velocity.y);  // move the enemy left or right
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Player") // if it isnt the player we hit something to bounce off of, and should change directions
        {
            dir = -dir;
            rb.AddForce(Vector3.right * dir * 100f); // add a little bump off the collider
            Instantiate(bumpFxPrefab, transform.position, Quaternion.identity);
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && dead == false)  // This is used to damage the player
        {
            other.gameObject.GetComponent<PlayerController>().isHit = true;
        }
    }


        private void OnTriggerEnter2D(Collider2D other) // This is used for the head hit trigger
    {
        if(other.gameObject.tag == "Player")
        {
            // essentially we are just doing a couple ground checks, and ensuring the player deliberatly landed on the enemy
            if(dead == false && other.gameObject.GetComponent<PlayerController>().grounded == false && other.gameObject.transform.position.y > transform.position.y)
            {
                Destroy(gameObject);
                Dead();
                dead = true;
                Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
                playerRb.velocity = Vector3.zero;
                playerRb.AddForce(Vector2.up * playerBounceForce); // bounce player
                other.gameObject.GetComponent<PlayerController>().ScreenShake();
               
            }
            
        }
    }

    public void Dead() // we can use this function to trigger any sort of death events we want
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); // spawn an explosion
    }
}
