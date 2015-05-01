using UnityEngine;
using System.Collections;

public class AssaultRiffle : MonoBehaviour
{

    public GameObject Bullet;
    private float fireRate = 0.1f;
    private float NextShot = 0.0f;
    private int ammo = 30;
    private bool wield;
    private GameObject player;
    private float timer;
    private int nbS;
    private float delay;
    string by;
    FollowPlayer attack;
    bool inactive;
    private bool thrown;

    void Start()
    {
        wield = false;
        nbS = 0;
        inactive = true;
        thrown = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        delay += Time.deltaTime;
        if (thrown)
        {
            if (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.z) < 10)
            {
                thrown = false;
                inactive = true;
            }

        }
        if (thrown == false && inactive)
            Inactive();
        if (wield)
        {
            transform.position = player.transform.position + player.transform.forward * 0.43f + new Vector3(0, -0.5f, 0);
            transform.rotation = player.transform.rotation;
            var rotation = Quaternion.LookRotation(player.transform.forward);

            if (Input.GetKey(KeyCode.Mouse0) && NextShot < Time.time && ammo > 0 && by == "player" && Ray())
            {
                Shot(delay, nbS, rotation);
                nbS += 1;
                delay = 0.0f;
                NextShot = Time.time + fireRate;
                ammo = ammo - 1;
            }
            else if (by == "enemy" && NextShot < Time.time && attack.attack && Ray())
            {
                Shot(delay, nbS, rotation);

                delay = 0.0f;
                NextShot = Time.time + fireRate;
                ammo = ammo - 1;

            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && timer > 0.1 && by == "player") 
            {
                wield = false;
                thrown = true;
                timer = 0f;
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
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("detect");
        }
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse1) && timer > 0.1)
        {
            ChoseG G = other.GetComponent<ChoseG>();
            if (gameObject == G.Select())
            {
                Debug.Log(gameObject + "=" + G.Select());
                by = "player";
                player = other.gameObject;
                timer = 0f;
                wield = true;
                inactive = false;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                other.GetComponent<PlayerController>().SetArmed(true);
                
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            //FollowPlayer enemy = other.GetComponent<FollowPlayer>();
            EnemyCondition hold = other.GetComponent<EnemyCondition>();
            ChoseG G = other.GetComponent<ChoseG>();
            if (hold.hold == false && gameObject == G.Select())
            {
                G.Deselect();
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

    void Shot(float delay, int shot, Quaternion rotation)
    {
        float ran = 0.0f;

        if (delay < 0.3f)
        {
            ran = Random.Range(5.0f - nbS * 0.1f, 5.0f + nbS * 0.1f);
        }
        else
        {
            nbS = 0;
        }

        GameObject bull = Instantiate(Bullet, transform.position + transform.forward, rotation) as GameObject;
        ARBullet Bull = bull.GetComponent<ARBullet>();
        Bull.started(new Vector3(0, ran, 0));
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
