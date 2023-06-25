using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Player : MonoBehaviour
{
    public bool Platform_isDead = false;
    public int Platform_curJumpNum = 0;

    [SerializeField]
    private GameObject pipeUI;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private int jumpNum = 3;
    [SerializeField]
    private float additionalJumpPower = 2;
    [SerializeField]
    private bool Platform_isGrounded = true;

    private Animator Platform_animator;
    private Rigidbody2D Platform_rigid;


    // Start is called before the first frame update
    void Start()
    {
        Platform_animator = GetComponent<Animator>();
        Platform_rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Platform_isDead)
        {
            Jump();
            UpdateAnimate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !Platform_isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f)
        {
            Platform_isGrounded = true;
            Platform_curJumpNum = 0;


            if(!Platform_isDead)
            {
                for (int i = 0; i < jumpNum; i++)
                {
                    pipeUI.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Platform_isGrounded = false;
    }

    void Jump()
    {
        if (Platform_curJumpNum == jumpNum)
            return;

        if(Input.GetKeyDown(KeyCode.Space) )
        {
            Platform_rigid.AddForce(Vector2.up * (jumpPower + Platform_curJumpNum * additionalJumpPower) , ForceMode2D.Impulse);
            Platform_curJumpNum++;
            pipeUI.transform.GetChild(jumpNum- Platform_curJumpNum).gameObject.SetActive(false);
        }
    }

    void UpdateAnimate()
    {
        Platform_animator.SetBool("Grounded", Platform_isGrounded);
    }


    void Die()
    {
        //사망 애니메이션
        Platform_animator.SetTrigger("Die");
        Platform_isDead = true;
        Platform_rigid.velocity = Vector2.zero;



        //사망 소리
        //게임매니저 사망트리거
        //데이터 저장
    }
}
