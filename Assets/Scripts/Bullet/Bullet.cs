using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public float speed;

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
    private void Update()
    {
        //raycast ile daha saglikli bir carpisma kontrolu
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * -1, out hit, 5))
        {
            if (hit.transform.gameObject.tag.Equals("Target"))
            {
                HitTarget(hit.transform.gameObject);
                Destroy(this.gameObject);
            }             
        }
    }
    void HitTarget(GameObject collision)
    {
        Debug.Log("isabet");
        collision.GetComponent<BulletDamage>().Damage();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Target"))
        {
            HitTarget(collision.gameObject);
            Destroy(this.gameObject);
        }            
    }
}
