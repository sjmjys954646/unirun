using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject NK_bulletPrefab;
    public float NK_spawnRateMin = 0.5f;
    public float NK_spawnRateMax = 3f;

    private Transform NK_target;
    private float NK_spawnRate;
    private float NK_timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        NK_timeAfterSpawn = 0f;
        NK_spawnRate = Random.Range(NK_spawnRateMin, NK_spawnRateMax);
        NK_target = FindObjectOfType<NK_PlayerController>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        NK_timeAfterSpawn += Time.deltaTime;
        if(NK_timeAfterSpawn >= NK_spawnRate)
        {
            NK_timeAfterSpawn = 0f;
            GameObject NK_bullet
                = Instantiate(NK_bulletPrefab, transform.position, transform.rotation);
            NK_bullet.transform.LookAt(NK_target);

            NK_spawnRate = Random.Range(NK_spawnRateMin, NK_spawnRateMax);
        }
    }
}
