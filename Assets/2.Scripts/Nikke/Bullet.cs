using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float NK_Bspeed = 8f;
    private Rigidbody2D NK_Brb;
    // Start is called before the first frame update
    void Start()
    {
        NK_Brb = GetComponent<Rigidbody2D>();
        NK_Brb.velocity = transform.forward * NK_Bspeed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            NK_PlayerController playerController = other.GetComponent<NK_PlayerController>();
            if(playerController != null)
            {
                playerController.Die();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
