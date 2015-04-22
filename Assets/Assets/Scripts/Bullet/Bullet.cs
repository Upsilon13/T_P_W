using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    int floorMask;
    float camRayLength = 100f;
    private float speed=10f;
    private int damage;

    public Bullet(int Damage, float speed)
    {
        damage = Damage;
        this.speed = speed;
    }

    /*void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 bulletToMouse = floorHit.point - transform.position;
            bulletToMouse.y = 0f;
            bulletToMouse = bulletToMouse.normalized;
            rigidbody.velocity = speed * bulletToMouse;
        }
    }
       void OnTriggerEnter(Collider other)
       {
           if (other.tag == "Enemy")
           {
               EnemyCondition health = other.GetComponent<EnemyCondition>();
               health.TakeDamage(damage);
           }
           Destroy(gameObject);
       }*/
    public void Colision(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyCondition health = other.GetComponent<EnemyCondition>();
            health.TakeDamage(damage);
        }
        if (other.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
    public void movement()
    {
        floorMask = LayerMask.GetMask("Floor");
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Vector3 bulletToMouse = floorHit.point - transform.position;
            Vector3 bulletToMouse = transform.forward;
            bulletToMouse.y = 0f;
            bulletToMouse = bulletToMouse.normalized;
            GetComponent<Rigidbody>().velocity = speed * bulletToMouse;
        }
    }
    } 




