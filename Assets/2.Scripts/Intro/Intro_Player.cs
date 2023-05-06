using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Player : MonoBehaviour
{
    private bool Intro_isGrounded = true;
    private Animator Intro_animator;


    // Start is called before the first frame update
    void Start()
    {
        Intro_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Intro_animator.SetBool("Grounded", Intro_isGrounded);
    }
}
