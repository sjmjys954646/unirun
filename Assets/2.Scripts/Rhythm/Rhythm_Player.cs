using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm_Player : MonoBehaviour
{
    public bool Rhythm_isDead = false;
    public int Rhythm_curJumpNum = 0;

    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private int jumpNum = 1;
    [SerializeField]
    private bool Rhythm_isGrounded = true;

    private Animator Rhythm_animator;
    private Rigidbody2D Rhythm_rigid;


    // Start is called before the first frame update
    void Start()
    {
        Rhythm_animator = GetComponent<Animator>();
        Rhythm_rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump();
        UpdateAnimate();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            Rhythm_isGrounded = true;
            Rhythm_curJumpNum = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rhythm_isGrounded = false;
    }

    void Jump()
    {
        if (Rhythm_curJumpNum == jumpNum)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rhythm_rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            Rhythm_curJumpNum++;
        }
    }

    void UpdateAnimate()
    {
        Rhythm_animator.SetBool("Grounded", Rhythm_isGrounded);
    }
}
