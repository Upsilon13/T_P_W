using UnityEngine;
using System.Collections;

public class PistolBullet : MonoBehaviour {
 
    

    void Start()
    {

        GetComponent<Rigidbody>().velocity = transform.forward * 30; 
    }
       void OnTriggerEnter(Collider other)
       {
           if (other.tag == "Enemy" && other.GetComponent<Rigidbody>().isKinematic == false)
           {
               EnemyCondition health = other.GetComponent<EnemyCondition>();
               health.TakeDamage(100);
               Destroy(gameObject);
           }
           else if (other.tag == "Player")
           {
               PlayerCondition health = other.GetComponent<PlayerCondition>();
               health.TakeDamage(1);
               Destroy(gameObject);
           }
           else if (other.tag != "Bullet" && other.tag != "Enemy")
           {
               Destroy(gameObject);
           }
       }

       void OnTriggerStay(Collider other)
       {
           if (other.tag != "Bullet" /*&& other.GetComponent<Rigidbody>().isKinematic == false*/)
           Destroy(gameObject);
       }
}
