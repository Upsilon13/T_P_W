﻿using UnityEngine;
using System.Collections;

public class ShotgunBullet : MonoBehaviour {

    Rigidbody bulletRigidbody;
    bool start = false;
    Vector3 tra;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (start)
        {
            bulletRigidbody.velocity = Quaternion.Euler(tra)* bulletRigidbody.transform.forward*30 ;
            start = false;
        }

    }

    public void started(Vector3 traj)
    {
        tra = traj;
        start = true; 
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" &&other.GetComponent<Rigidbody>().isKinematic==false)
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
