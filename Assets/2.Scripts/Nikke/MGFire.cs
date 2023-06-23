using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGFire : MonoBehaviour
{
    public enum State
    {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading // ������ ��
    }
    public State state { get; private set; } // ���� ���� ����
    // Start is called before the first frame update
    //public Transform spawnPoint;
    public float distance = 15f;

    public ParticleSystem muzzleFlash;
    public ParticleSystem shellEject;
    public GameObject impact;

    Camera camera;
    bool isFiring;
    public float rateOffire = 0.1f;

    private AudioSource gunAudioPlayer; // �� �Ҹ� �����
    public AudioClip shotClip; // �߻� �Ҹ�
    public AudioClip reloadClip; // ������ �Ҹ�

    public float damage = 25; // ���ݷ�

    public int ammoRemain = 100; // ���� ��ü ź��
    public int magCapacity = 25; // źâ �뷮
    public int magAmmo; // ���� źâ�� �����ִ� ź��

    public float timeBetFire = 0.12f; // �Ѿ� �߻� ����
    public float reloadTime = 1.8f; // ������ �ҿ� �ð�
    private float lastFireTime; // ���� ���������� �߻��� ����


    private void Awake()
    {
        // ����� ������Ʈ���� ������ ��������
        gunAudioPlayer = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        // ���� źâ�� ����ä���
        magAmmo = magCapacity;
        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        // ���������� ���� �� ������ �ʱ�ȭ
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
                // ������ �� �߻� ������ ����
                lastFireTime = Time.time;
                // ���� �߻� ó�� ����
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

            // �������� ���� IDamageable ������Ʈ�� �������µ� �����ߴٸ�
            if (target != null)
            {
                // ������ OnDamage �Լ��� ������Ѽ� ���濡�� ������ �ֱ�
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
            // źâ�� ���� ź���� ���ٸ�, ���� ���� ���¸� Empty���� ����
            state = State.Empty;
        }
    }

    private IEnumerator ShotEffect()
    {
        // �ѱ� ȭ�� ȿ�� ���
        muzzleFlash.Play();
        // ź�� ���� ȿ�� ���
        shellEject.Play();

        // �Ѱ� �Ҹ� ���
        gunAudioPlayer.PlayOneShot(shotClip);

        // 0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);
    }

    public bool Reload()
    {
        if (state == State.Reloading ||
            ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            // �̹� ������ ���̰ų�, ���� �Ѿ��� ���ų�
            // źâ�� �Ѿ��� �̹� ������ ��� ������ �Ҽ� ����
            return false;
        }

        // ������ ó�� ����
        StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        // ���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;
        // ������ �Ҹ� ���
        gunAudioPlayer.PlayOneShot(reloadClip);


        // ������ �ҿ� �ð� ��ŭ ó���� ����
        yield return new WaitForSeconds(reloadTime);

        // źâ�� ä�� ź���� ����Ѵ�
        int ammoToFill = magCapacity - magAmmo;

        // źâ�� ä������ ź���� ���� ź�ຸ�� ���ٸ�,
        // ä������ ź�� ���� ���� ź�� ���� ���� ���δ�
        if (ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        // źâ�� ä���
        magAmmo += ammoToFill;
        // ���� ź�࿡��, źâ�� ä�ŭ ź���� �A��
        ammoRemain -= ammoToFill;

        // ���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}

