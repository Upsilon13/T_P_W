using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour
{


    public GameObject Bullet;
    private float fireRate = 0.2f;
    private float NextShot = 0.0f;
    private int ammo = 8;
    private bool wield;
    private Transform player;
    private float timer;
    private string by;
    FollowPlayer attack;
    private bool thrown;
    bool inactive;

    void Awake()
    {
        wield = false;
        timer = 0f;
        thrown = false;
        inactive = true;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (thrown)
        {
            if (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.z) < 10)
                thrown = false;
        }
        if (thrown == false && inactive)
        {
            Inactive();
            inactive = true;
        }
        if (wield)
        {
            transform.position = player.transform.position + player.transform.forward * 0.2f + new Vector3(0, -0.5f, 0);
            transform.rotation = player.transform.rotation;



            if (by == "player" && Input.GetKeyDown(KeyCode.Mouse0) && NextShot < Time.time && ammo > 0)
            {
                Instantiate(Bullet, transform.position + transform.forward, player.rotation);
                NextShot = Time.time + fireRate;
                ammo = ammo - 1;
            }
            else if (by == "enemy" && NextShot < Time.time && attack.attack)
            {
                Instantiate(Bullet, transform.position + transform.forward, player.rotation);
                NextShot = Time.time + fireRate;

            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && by == "player" && timer > 0.1)
            {
                thrown = true;
                timer = 0;
                wield = false;
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;
                GetComponent<Rigidbody>().velocity = transform.forward * 15;
                gameObject.transform.rotation = (new Quaternion(45, 45, 45, 45));
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse1) && timer > 0.1)
        {
            by = "player";
            timer = 0;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            wield = true;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            inactive = false;

        }
        else if (other.gameObject.tag == "Enemy" && thrown == false)
        {
            //FollowPlayer enemy = other.GetComponent<FollowPlayer>();
            EnemyCondition hold = other.GetComponent<EnemyCondition>();
            if (hold.hold == false)
            {
                attack = other.GetComponent<FollowPlayer>();
                by = "enemy";
                player = other.transform;
                //player = other.transform;
                timer = 0f;
                wield = true;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && wield == false)
        {
            float speed = Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.z);
            if (speed > 15)
            {
                EnemyCondition stun = other.GetComponent<EnemyCondition>();
                stun.Stun();
            }
            //Debug.Log("enemy");
        }
        else if (other.tag == "Floor")
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, 0, gameObject.GetComponent<Rigidbody>().velocity.z);
            Debug.Log("floor");
        }
        /*else if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Player")
        {
            gameObject.GetComponent<Rigidbody>().velocity = -gameObject.GetComponent<Rigidbody>().velocity * 0.8f;
            Debug.Log("fuck");
        }*/
        else if (other.tag == "Wall")
        {
            gameObject.GetComponent<Rigidbody>().velocity = -gameObject.GetComponent<Rigidbody>().velocity * 0.2f;
            Debug.Log("wall");
        }
    }
    void Inactive()
    {
        //GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.rotation = (new Quaternion(45, 45, 45, 45));
    }


}
