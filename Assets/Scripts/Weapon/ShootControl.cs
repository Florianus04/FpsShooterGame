using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootControl : MonoBehaviour
{
    [Header("Object References")]
    public GameObject bulletPrefab;//mermi prefabi
    public GameObject casingPrefab;//bos kovan prefabi
    public GameObject bulletSmoke;//duman
    public GameObject bulletLight;//isik

    [Header("Transform References")]
    public Transform bulletSpawnPoint;//mermi cikis noktasi
    public Transform shootTargetPoint;//atis hedefi
    public Transform casingPoint;//bos kovan cikis noktasi
    public Transform casingTarget;//bos kovan hedefi

    [Header("UI References")]
    public Text currentAmmoText;
    public Text shootModeText;
    public Text sightModeText;

    [Header("Other References")]
    public Animator sightAnimator;
    public Animator parentWeapon;
    public Animator cameraAnimator;
    public CameraAnims camera;
    

    [Header("Sounds")]
    public AudioClip reloadSound;
    public AudioClip shootSound;
    public AudioClip emptySound;

    [Header("Weapon Settings")]
    public int maxAmmo = 32;
    public int currentAmmo;

    public float shootSpeed = 0.8f;//atis hizi
    public float reloadTime = 1.5f;//reload suresi
    public float bulletSpeed = 50f;//mermi cikis hizi

    public bool isAuto = true;//atis modu
    public bool sightOn = false;//nisangah ayari

    private bool isReloading = false;

    private AudioSource audioSource;
    private Animator animator;

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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(currentAmmo > 0)//mermi varsa
            {
                //atis yap
                if (isAuto)
                {
                    //tam otomatik atis
                    ShootAuto();
                }
                else
                {
                    //yari otomatik atis
                    ShootSemi();
                }
            }
            else//mermi yoksa
            {
                //bosa tetik dusurme sesi
                Empty();
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //atisi durdur verileri sifirla
            StopAllCoroutines();
            this.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            bulletLight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            parentWeapon.SetBool("Aim", true);
            cameraAnimator.SetBool("Aim", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            parentWeapon.SetBool("Aim", false);
            cameraAnimator.SetBool("Aim", false);
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
            //nisangah ayari
            sightOn = !sightOn;
        }
        if (sightOn)
        {
            //uzak mesafeye atis icin ayarla
            sightAnimator.SetBool("SightOn", true);
            this.gameObject.transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }
        else
        {
            //yakin mesafeye atis icin ayarla
            sightAnimator.SetBool("SightOn", false);
            this.gameObject.transform.localRotation = Quaternion.Euler(1f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
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
        StartCoroutine(Light());//namludan isik cikar

        animator.SetTrigger("Shoot");//atis animasyomumu oynat
        this.gameObject.GetComponent<CameraAnims>().Shake(0.05f, 0.05f);

        audioSource.PlayOneShot(shootSound, 1f);//atis sesi   
        
        GameObject smoke = Instantiate(bulletSmoke, bulletSpawnPoint.position, bulletSmoke.transform.rotation);//duman uret
        Destroy(smoke, 2f);//yok et
        
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletPrefab.transform.rotation);//mermi uret
        bullet.GetComponent<Bullet>().speed = bulletSpeed;//mermi hizini belirle
        bullet.GetComponent<Bullet>().target = shootTargetPoint;//mermi hedefini belirle

        GameObject casingBullet = Instantiate(casingPrefab, casingPoint.position, casingPrefab.transform.rotation);//bos kovan at
        casingBullet.GetComponent<CasingBullet>().target = casingTarget;//bos kovan hedefini belirle

        currentAmmo -=1;//sarjorden mermi azalt
    }
    void Empty()
    {
        audioSource.PlayOneShot(emptySound, 1f);
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
        isReloading = true;

        audioSource.PlayOneShot(reloadSound, 1f);//reload sesi cal
        animator.SetTrigger("Reload");//animasyonu oynat

        yield return new WaitForSeconds(reloadTime);//bekle

        currentAmmo = maxAmmo;//mermiyi yenile

        isReloading = false;
    }
}
