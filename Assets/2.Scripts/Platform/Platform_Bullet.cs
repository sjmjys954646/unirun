using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Bullet : MonoBehaviour
{
    private Platform_Player player;
    Vector2 v2 = new Vector2(1, 0);
    public float Platform_bulletspeed = 10f;

    private void Start()
    {
        player = Platform_GameManager.Instance.player.GetComponent<Platform_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10)
        {
            if(!player.Platform_isDead)
            {
                Platform_GameManager.Instance.addScore(1);
            }
            Destroy(gameObject);
        }

        transform.Translate(v2 * Platform_bulletspeed * Time.deltaTime);
    }
}
