using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float speed; // speed of the enemy

    [SerializeField]
    private int startingDirection; // used to make more interesting level layouts

    [SerializeField]
    private GameObject explosionPrefab; // death explosion


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
            rb.AddForce(Vector3.up * 50f);
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
            // essentially we are just doing a couple ground checks, and ensuring the player deliberatly jumped to kill the enemy
            if(dead == false && other.gameObject.GetComponent<PlayerController>().grounded == false && other.gameObject.transform.position.y > transform.position.y)
            {
                Destroy(gameObject);
                Dead();
                dead = true;
            }
            
        }
    }

    public void Dead() // we can use this function to trigger any sort of death events we want
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); // spawn an explosion
    }
}
