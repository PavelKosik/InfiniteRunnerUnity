using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    public float moveSpeedSideways;
    private Rigidbody rb;
    public float moveSpeedForward;
    public float jumpSpeed;
    public float jumpTime;
    private float currentJumpTime;
    public float slideTime;
    private float currentSlideTime;
    private Animator anim;
    private bool isJumping = false;
    private bool isSliding = false;
    public float speedMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //player always moves forward
        moveSpeedForward += Time.deltaTime * speedMultiplier;

        //handles horizontal input from the player
        int horizontal = 0;
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }
        //handles if player is jumping
        HandleJumping();

        //handles if player is sliding
        HandleSliding();

        //moves player based on the input
        rb.velocity = new Vector3(horizontal * moveSpeedSideways * Time.deltaTime, rb.velocity.y, moveSpeedForward * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if player collides with ground he is no longer in the air
        //that means that he is no longer jumping
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
        }
    }

    void HandleJumping()
    {
        //player can use the space or w key to jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            //player can only jump if he is not currently jumping
            if (!anim.GetBool("isJumping"))
            {
                isJumping = true;
                currentJumpTime = jumpTime;
                anim.SetBool("isJumping", true);
            }
        }
        //if player is jumping his velocity is made to reflect that
        if (isJumping)
        {
            //this makes player go up for a set period of time
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed * Time.deltaTime, rb.velocity.z);
            currentJumpTime -= Time.deltaTime;

            //makes player fall back to the ground
            if (currentJumpTime <= 0.0f)
            {
                isJumping = false;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
        }

    }

    void HandleSliding()
    {
        //player can slide only using s key
        if (Input.GetKeyDown(KeyCode.S))
        {
            //player can only slide if he isn't slide at the moment
            if (!isSliding)
            {
                isSliding = true;
                currentSlideTime = slideTime;
                anim.SetBool("isSliding", true);
            }

        }

        //sliding is done by changing the hitbox of a player in unity animator
        if (isSliding)
        {
            //makes sure that player doesn't slide forever
            currentSlideTime -= Time.deltaTime;

            if (currentSlideTime <= 0.0f)
            {
                isSliding = false;
                anim.SetBool("isSliding", false);
            }

        }
    }
}


