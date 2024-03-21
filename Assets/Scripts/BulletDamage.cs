using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public GameObject damageEffect;
    public AudioClip damageSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Debug.Log("isabet");
            GameObject smoke = Instantiate(damageEffect, collision.transform.position, damageEffect.transform.rotation);
            Destroy(smoke, 2f);
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(damageSound, 1f);           
        }
    }
}
