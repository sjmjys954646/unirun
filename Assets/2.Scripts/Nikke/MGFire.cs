using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGFire : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnPoint;
    public float distance = 15f;

    public ParticleSystem muzzleFlash;
    //public GameObject impact;

    Camera camera;
    bool isFiring;
    float shotCounter;
    public float rateOffire = 0.1f;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            isFiring = true;
        else if (Input.GetButtonUp("Fire1"))
            isFiring = false;

        if (isFiring)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = rateOffire;
                Shoot();
            }
        }
        else
            shotCounter -= Time.deltaTime;
    }

    private void Shoot()
    {
        RaycastHit hit;

        muzzleFlash.Play();

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance))
        {
            Debug.Log("hit");

            //Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
            Debug.Log("Not hit");
    }
}

/*
public class MGFire : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 15f;
    public float mag = 100;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    private object hit;
    //public GameObject impactEffect;
    public float impactForce = 30f;
    private float nextTimeToFire = 0f;

    void Start()
    {
        fpsCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
                nextTimeToFire = Time.time + 1f / fireRate;
                shoot();
        }
        else if(Input.GetButtonUp("Fire1"))
            muzzleFlash.Stop();

    }

    void shoot()
    {

        muzzleFlash.Play();
        StartCoroutine(ShotEffect());
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("hit");
            
            if (hit.rigidbody != null)
            { 
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            
        }
        

        //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(impactGO, 2f);

    }
    private IEnumerator ShotEffect()
    {
        // 총구 화염 효과 재생
        muzzleFlash.Play();
        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

    }
}*/
