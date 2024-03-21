using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletDamage : MonoBehaviour
{
    public AudioClip damageSound;
    public string distance;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Buttons.shootConsole.gameObject.GetComponent<Text>().text = ("Shoot " + distance + " mt");
            Buttons.shootCountText.gameObject.GetComponent<Text>().text = ("Shoot Count : " + Buttons.shootCount);
            Buttons.shootCount += 1;
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(damageSound, 1f);           
        }
    }
}
