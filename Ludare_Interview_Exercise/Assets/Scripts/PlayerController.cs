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
    private float jumpDuration;

    [SerializeField]
    private float invincibliltyTime;

    [SerializeField]
    private SpriteRenderer sprite;

    // --------Back End Variables------------

    [Header("Back End Variables")]
    public Transform raycastPoint; // An empty Gameobject used to track where the raycast for jumping ends
    private Rigidbody2D rb; // Players rigidbody
    public bool grounded;
    private float x;
    private float jumpTimer;
    private float originalGravity;
    private bool isJumping;
    private int numCurrentJumps;
    public bool isHit;
    private float invincibilityTimer;
    private bool flashing;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        if(originalColor.a <=0)
        {
            originalColor = new Color(1, 1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, raycastPoint.position, groundLayer); // Checks when we're on the ground or not, and saves to bool

        x = Input.GetAxis("Horizontal"); // get horizontal input

        if (grounded == true) // check if on the ground
        {
           // isJumping = false;
            numCurrentJumps = numberOfJumps; // reset jump counter
        }

        if (Input.GetButtonDown("Fire1") && numCurrentJumps >0) // add short hop
        {
            jumpTimer = jumpDuration;
            //rb.velocity = Vector3.zero;
            rb.velocity = Vector2.up * jumpHeight;
            isJumping = true;
            numCurrentJumps -= 1;
        }

        if(isJumping == true && Input.GetButton("Fire1")) // hold for higher jump
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

        if(Input.GetButtonUp("Fire1")) // cancel jump
        {
            jumpTimer = 0;
            isJumping = false;
            
        }

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + x * speed * Time.deltaTime, transform.position.y, transform.position.z); // move the player

        if(jumpTimer>0)
        {
            jumpTimer -= Time.fixedDeltaTime; // decrease our jump timer
        }

        if(isHit == true) // if you are hit
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


       
    }

   

   
}
