using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_randomDispensor : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float curtime = 0;
    [SerializeField]
    private float nextSpawnTime = 3f;
    [SerializeField]
    private float nextMinimumSpawnTime = 1.5f;
    [SerializeField]
    private float nextMaximumSpawnTime = 3f;
    [SerializeField]
    private float randomMinimumSpeed = 7f;
    [SerializeField]
    private float randomMaximumSpeed = 13f;
    [SerializeField]
    private float randomMinimumY = -2f;
    [SerializeField]
    private float randomMaximumY = 4f;

    // Update is called once per frame
    void Update()
    {
        if(curtime >= nextSpawnTime)
        {
            spawn();
            setNexttime();
            curtime = 0;
        }

        curtime += Time.deltaTime;
    }

    void spawn()
    {
        float randomSpeed = Random.Range(randomMinimumSpeed, randomMaximumSpeed);
        float randomY = Random.Range(0f, 3f);
        Vector3 pos = transform.position;
        pos.y = randomMinimumY + randomY;
        GameObject curBullet = Instantiate(bullet, pos, Quaternion.identity);
        curBullet.GetComponent<Platform_Bullet>().Platform_bulletspeed = randomSpeed;
        curBullet.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void setNexttime()
    {
        nextSpawnTime = Random.Range(nextMinimumSpawnTime, nextMaximumSpawnTime);
    }

}
