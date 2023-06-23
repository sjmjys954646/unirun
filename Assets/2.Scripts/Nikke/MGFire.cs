using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGFire : MonoBehaviour
{
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }
    public State state { get; private set; } // 현재 총의 상태
    // Start is called before the first frame update
    //public Transform spawnPoint;
    public float distance = 15f;

    public ParticleSystem muzzleFlash;
    public ParticleSystem shellEject;
    public GameObject impact;

    Camera camera;
    bool isFiring;
    public float rateOffire = 0.1f;

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 25; // 공격력

    public int ammoRemain = 100; // 남은 전체 탄약
    public int magCapacity = 25; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약

    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
    private float lastFireTime; // 총을 마지막으로 발사한 시점


    private void Awake()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        // 현재 탄창을 가득채우기
        magAmmo = magCapacity;
        // 총의 현재 상태를 총을 쏠 준비가 된 상태로 변경
        state = State.Ready;
        // 마지막으로 총을 쏜 시점을 초기화
        lastFireTime = 0;
    }


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
            /*
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = rateOffire;
                Shoot();
            }
            */
            if (state == State.Ready
            && Time.time >= lastFireTime + timeBetFire)
            {
                // 마지막 총 발사 시점을 갱신
                lastFireTime = Time.time;
                // 실제 발사 처리 실행
                Shoot();


            }
        }
        if (state == State.Empty || Input.GetKeyDown(KeyCode.R))
            Reload();

    }

    private void Shoot()
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;



        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance))
        {
            Debug.Log("hit");

            //Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            IDamageable target =
               hit.collider.GetComponent<IDamageable>();

            // 상대방으로 부터 IDamageable 오브젝트를 가져오는데 성공했다면
            if (target != null)
            {
                // 상대방의 OnDamage 함수를 실행시켜서 상대방에게 데미지 주기
                target.OnDamage(damage, hit.point, hit.normal);
            }
            
            hitPosition = hit.point;
            GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
        {
            Debug.Log("Not hit");
        }

        StartCoroutine(ShotEffect());

        //StopAllCoroutines();

        magAmmo--;
        if (magAmmo <= 0)
        {
            // 탄창에 남은 탄약이 없다면, 총의 현재 상태를 Empty으로 갱신
            state = State.Empty;
        }
    }

    private IEnumerator ShotEffect()
    {
        // 총구 화염 효과 재생
        muzzleFlash.Play();
        // 탄피 배출 효과 재생
        shellEject.Play();

        // 총격 소리 재생
        gunAudioPlayer.PlayOneShot(shotClip);

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);
    }

    public bool Reload()
    {
        if (state == State.Reloading ||
            ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            // 이미 재장전 중이거나, 남은 총알이 없거나
            // 탄창에 총알이 이미 가득한 경우 재장전 할수 없다
            return false;
        }

        // 재장전 처리 시작
        StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;
        // 재장전 소리 재생
        gunAudioPlayer.PlayOneShot(reloadClip);


        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(reloadTime);

        // 탄창에 채울 탄약을 계산한다
        int ammoToFill = magCapacity - magAmmo;

        // 탄창에 채워야할 탄약이 남은 탄약보다 많다면,
        // 채워야할 탄약 수를 남은 탄약 수에 맞춰 줄인다
        if (ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        // 탄창을 채운다
        magAmmo += ammoToFill;
        // 남은 탄약에서, 탄창에 채운만큼 탄약을 뺸다
        ammoRemain -= ammoToFill;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }
}

