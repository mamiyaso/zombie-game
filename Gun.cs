using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public enum FireMode
    {
        Single,
        Burst,
    }

    public bool IsReloading
    {
        get { return isReloading; }
    }

    public FireMode fireMode = FireMode.Single;
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 200f;
    public float fireRate = 15f;
    
    public int maxAmmo = 20;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject crosshair;
    public AudioClip[] shootingSounds;
    public AudioClip forendSound;

    private WeaponSwitching weaponSwitching;
    private float nextTimeToFire = 0f;

    public Animator animator;

    public Vector3[] burstDirections = {
        new Vector3(0.09f, 0.5f, 0f),//sag ust
        new Vector3(0.6f, -0.5f, 0f),//sag alt
        new Vector3(0.03f, 0.5f, 0f),//ort sag ust
        new Vector3(0f, -0.5f, 0f),//ort alt
        new Vector3(-0.03f, 0.5f, 0f),//ort sol ust
        new Vector3(-0.6f, -0.5f, 0f),//sol alt
        new Vector3(-0.9f, 0.5f, 0f),//sol ust
    };
    public int burstShotCount = 7;
    


    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reload", false);
        crosshair.SetActive(true);
    }

    private void OnDisable()
    {
        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }
    }

    void Update()
    {
        
        if (isReloading || PlayerHealth.isDead)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            if (fireMode == FireMode.Single)
            {
                PlayShootingSound();
                Shoot();
            }
            else if (fireMode == FireMode.Burst)
            {
                PlayShootingSound();
                BurstShoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine (Reload());
        }

    }

    IEnumerator Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reload", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reload", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading =false;
        }
    }

    IEnumerator Recoil()
    {
        animator.SetBool("Recoil", true);
        yield return new WaitForSeconds(.20f);
        animator.SetBool("Recoil", false);
    }


    void Shoot()
    {
        currentAmmo--;
        FireRay(fpsCam.transform.forward);

        StartCoroutine(Recoil());
    }

    void BurstShoot()
    {
        currentAmmo--;

        foreach (var direction in burstDirections)
        {
            Vector3 shootDirection = fpsCam.transform.forward + direction;
            FireRay(shootDirection);
        }
        StartCoroutine(Recoil());
    }

    void FireRay(Vector3 direction)
    {   
        muzzleFlash.Play();
        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, layerMask))
        {
            Debug.Log(hit.transform.name);

            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    void PlayShootingSound()
    {
        AudioSource centralAudioSource = WeaponSwitching.centralAudioSource;
        if (centralAudioSource != null)
        {
            if (shootingSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, shootingSounds.Length);
                centralAudioSource.clip = shootingSounds[randomIndex];
                centralAudioSource.Play();
            }

            if (forendSound != null)
            {
                centralAudioSource.PlayOneShot(forendSound);
            }
        }
    }
}