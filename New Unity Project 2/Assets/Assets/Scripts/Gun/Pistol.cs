using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour
{


    public GameObject Bullet;
    private float fireRate = 0.2f;
    private float NextShot = 0.0f;
    private int ammo = 8;
    private bool wield;
    //private Transform player;
    private float timer;
    private string by;
    FollowPlayer attack;
    private bool thrown;
    bool inactive;
    GameObject player;

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



            if (by == "player" && Input.GetKeyDown(KeyCode.Mouse0) && NextShot < Time.time && ammo > 0 && Ray())
            {
                Instantiate(Bullet, transform.position + transform.forward, player.transform.rotation);
                NextShot = Time.time + fireRate;
                ammo = ammo - 1;
            }
            else if (by == "enemy" && NextShot < Time.time && attack.attack && Ray())
            {
                Instantiate(Bullet, transform.position + transform.forward, player.transform.rotation);
                NextShot = Time.time + fireRate;

            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && by == "player" && timer > 0.1)
            {
                thrown = true;
                timer = 0;
                wield = false;
                player.GetComponent<PlayerController>().SetArmed(false);
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;
                GetComponent<Rigidbody>().velocity = transform.forward * 15;
                gameObject.transform.rotation = (new Quaternion(45, 45, 45, 45));
                player.GetComponent<ChoseG>().Deselect();
            }
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse1) && timer > 0.1)
        {
            ChoseG G = other.GetComponent<ChoseG>();
            if (gameObject == G.Select())
            {
                Debug.Log(gameObject + "=" + G.Select());
                by = "player";
                timer = 0;
                player = GameObject.FindGameObjectWithTag("Player");
                wield = true;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                inactive = false;
                other.GetComponent<PlayerController>().SetArmed(true);


            }

        }
        else if (other.gameObject.tag == "Enemy" && thrown == false)
        {
            //FollowPlayer enemy = other.GetComponent<FollowPlayer>();
            EnemyCondition hold = other.GetComponent<EnemyCondition>();
            ChoseG G = other.GetComponent<ChoseG>();
            if (hold.hold == false && gameObject == G.Select())
            {
                attack = other.GetComponent<FollowPlayer>();
                by = "enemy";
                player = other.gameObject;
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

    private bool Ray()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 1))
        {
            if (hit.collider.tag == ("Wall"))
                return false;
            else
                return true;
        }
        else
            return true;
    }
}
