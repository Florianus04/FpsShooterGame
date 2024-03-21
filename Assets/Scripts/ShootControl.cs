using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    public GameObject bulletPrefab;//mermi prefabi
    public GameObject casingPrefab;//bos kovan prefabi
    public GameObject bulletSmoke;//duman
    public GameObject bulletLight;//isik
    public CameraAnims camera;

    public Transform bulletSpawnPoint;//mermi cikis noktasi
    public Transform shootTargetPoint;//atis hedefi
    public Transform casingPoint;//bos kovan cikis noktasi
    public Transform casingTarget;//bos kovan hedefi

    public AudioClip reloadSound, shootSound;

    public int maxAmmo = 32;
    public int currentAmmo;

    public float shootSpeed = 0.8f;
    public float reloadTime = 1.5f;
    public float bulletSpeed = 50f;//mermi cikis hizi
    public bool isAuto = true;
    public bool sightOn = false;

    private AudioSource audioSource;
    private Animator animator;
    public Animator sightAnimator;
    
    
    private bool isReloading = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
        {
            //atis yap
            if (isAuto)
            {
                ShootAuto();
            }
            else
            {
                ShootSemi();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //atisi durdur
            StopAllCoroutines();
            bulletLight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            //reload fonksiyonu
            StartCoroutine(Reload());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //atis modu belirle
            isAuto = !isAuto;
        }

        SightControl();
    }
    void SightControl()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            sightOn = !sightOn;
        }
        if (sightOn)
        {
            sightAnimator.SetBool("SightOn", true);
            this.gameObject.transform.localRotation = Quaternion.Euler(-0.8f, transform.rotation.y, transform.rotation.z);
        }
        else
        {
            sightAnimator.SetBool("SightOn", false);
            this.gameObject.transform.localRotation = Quaternion.Euler(0f, transform.rotation.y, transform.rotation.z);
        }
    }
    void ShootAuto()
    {
        //tam otomatik atis
        StartCoroutine(ShootEnum());
    }
    void ShootSemi()
    {
        //yari otomatik atis
        Shoot();
    }
    void Shoot()
    {
        StartCoroutine(Light());
        animator.SetTrigger("Shoot");
        audioSource.PlayOneShot(shootSound, 1f);//atis sesi   
        GameObject smoke = Instantiate(bulletSmoke, bulletSpawnPoint.position, bulletSmoke.transform.rotation);//duman uret
        Destroy(smoke, 2f);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletPrefab.transform.rotation);//mermi uret
        bullet.GetComponent<Bullet>().speed = bulletSpeed;//mermi hizini belirle
        bullet.GetComponent<Bullet>().target = shootTargetPoint;//mermi hedefini belirle

        GameObject casingBullet = Instantiate(casingPrefab, casingPoint.position, casingPrefab.transform.rotation);//bos kovan at
        casingBullet.GetComponent<CasingBullet>().target = casingTarget;


        currentAmmo -=1;//sarjorden mermi azalt
    }
    IEnumerator Light()
    {
        bulletLight.SetActive(true);
        yield return new WaitForSeconds(0.01f);//bekle
        bulletLight.SetActive(false);
    }
    IEnumerator ShootEnum()
    {
        while (currentAmmo > 0)
        {
            Shoot();
            yield return new WaitForSeconds(shootSpeed);//bekle
        }
       
    }
    IEnumerator Reload()
    {
        audioSource.PlayOneShot(reloadSound, 1f);//reload sesi cal
        animator.SetTrigger("Reload");//animasyonu oynat
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);//bekle

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
