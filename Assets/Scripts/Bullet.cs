using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public float speed = 50f;

    public float lifeTime = 5f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Hedef belirtilmedi!");
            return;
        }

        //hedefi belirle
        Vector3 direction = (target.position - transform.position).normalized;
        //hedefe dogru kuvvet uygula
        GetComponent<Rigidbody>().velocity = direction * speed;
        //mermiyi omru bitince yok et
        Destroy(gameObject, lifeTime);
    }
}