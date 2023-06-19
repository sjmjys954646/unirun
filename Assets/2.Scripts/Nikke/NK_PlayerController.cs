using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    private float NK_horizontal;
    private float NK_speed = 8f;
    private float NK_jumpForce = 10f;
    private bool NK_isDead = false;
    private bool NK_isGrounded = false;
    private bool NK_isFacingRight = true;

    private Rigidbody2D NK_playerRigidbody;
    private Animator NK_animator;
    private AudioSource NK_playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        NK_playerRigidbody = GetComponent<Rigidbody2D>();
        NK_animator = GetComponent<Animator>();
        NK_playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (NK_isDead) { return; }

        NK_horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && NK_isGrounded)
        {
            NK_playerRigidbody.velocity = new Vector2(NK_playerRigidbody.velocity.x, NK_jumpForce);
        }

        
        if (Input.GetKey("Jump") && NK_playerRigidbody.velocity.y > 0f)
        {
            NK_playerRigidbody.velocity = new Vector2(NK_playerRigidbody.velocity.x, NK_playerRigidbody.velocity.y * 0.5f);
        }


        //NK_animator.SetBool("Grounded", NK_isGrounded);
        Flip();
    }

    private void FixedUpdate()
    {
        NK_playerRigidbody.velocity = new Vector2(NK_horizontal * NK_speed, NK_playerRigidbody.velocity.y);
    }

    private void Flip()
    {
        if (NK_isFacingRight && NK_horizontal < 0f || !NK_isFacingRight && NK_horizontal > 0f)
        {
            NK_isFacingRight = !NK_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


public void Die()
    {
        NK_animator.SetTrigger("Die");
        NK_playerAudio.clip = deathClip;
        NK_playerAudio.Play();
        NK_playerRigidbody.velocity = Vector2.zero;
        NK_isDead = true;
    }
    //private bool IsGrounded()
    //{
    //    return Physics2D.OverlapCircle(NK_groundCheck.position, 0.2f, NK_groundLayer);
    //}

    private void OnTrigerEnter2D(Collider2D other)
    {
        if(other.tag == "Dead" && !NK_isDead) { Die(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f)
        {
            NK_isGrounded = true;
            Debug.Log("grounded");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        NK_isGrounded = false;
    }
}



