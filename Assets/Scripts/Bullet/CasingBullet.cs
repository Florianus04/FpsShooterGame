using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingBullet : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;

    void Start()
    {
        if (target != null)
        {
            //random rotation ver
            float randomPitch = Random.Range(-90f, 90f);
            float randomYaw = Random.Range(-180f, 180f);
            float randomRoll = Random.Range(-90f, 90f);

            Quaternion randomRotation = Quaternion.Euler(randomPitch, randomYaw, randomRoll);

            transform.rotation = randomRotation;

            //firlat
            Vector3 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody>().velocity = direction * speed; // speed, objenin hareket hýzýdýr
        }
        else
        {
            Debug.LogError("Hedef belirtilmemiþ!");
        }
    }
}
