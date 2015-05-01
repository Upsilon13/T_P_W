using UnityEngine;
using System.Collections;

public class Shotgun : MonoBehaviour
{

    public GameObject Bullet;
    private float fireRate = 0.5f;
    private float NextShot = 0.0f;
    private int ammo = 6;
    private bool wield;
    private GameObject player;
    private float timer;
    string by;
    FollowPlayer attack;
    private bool thrown;
    bool inactive;

    void Start()
    {
        wield = false;
        timer = 0;
        thrown = false;
        inactive = true;
    }

    void Update()
    {
        timer += Time.deltaTime;
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
            transform.position = player.transform.position + player.transform.forward * 0.2f + new Vector3(0, -0.1f, 0);
            transform.rotation = player.transform.rotation;

            var rotation = Quaternion.LookRotation(player.transform.forward);

            if (Input.GetKeyDown(KeyCode.Mouse0) && NextShot < Time.time && ammo > 0 && by == "player" && Ray())
            {
                //Instantiate(Bullet, transform.position + transform.forward, player.rotation);
                Shot(rotation);

                NextShot = Time.time + fireRate;
                ammo = ammo - 1;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && by == "player" && timer > 0.1)
            {
                timer = 0f;
                wield = false;
                player.GetComponent<PlayerController>().SetArmed(false);
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;
                GetComponent<Rigidbody>().velocity = transform.forward * 15;
                gameObject.transform.rotation = (new Quaternion(45, 45, 45, 45));
                player.GetComponent<ChoseG>().Deselect();
            }

            else if (by == "enemy" && NextShot < Time.time && attack.attack && Ray())
            {
                Shot(rotation);
                NextShot = Time.time + fireRate;
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse1) && timer > 0.1)
        {
            ChoseG G = other.GetComponent<ChoseG>();
            if (gameObject == G.Select())
            {
                Debug.Log(gameObject + "=" + G.Select());
                by = "player";
                player = other.gameObject;
                inactive = false;
                timer = 0f;
                wield = true;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                other.GetComponent<PlayerController>().SetArmed(true);
            }
        }
        else if (other.gameObject.tag == "Enemy")
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



    void Shot(Quaternion rotation)
    {
        GameObject bull1 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull1 = bull1.GetComponent<ShotgunBullet>();
        Bull1.started(new Vector3(0, 0, 0));
        GameObject bull2 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull2 = bull2.GetComponent<ShotgunBullet>();
        Bull2.started(new Vector3(0, 5, 0));
        GameObject bull3 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull3 = bull3.GetComponent<ShotgunBullet>();
        Bull3.started(new Vector3(0, 10, 0));
        GameObject bull4 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull4 = bull4.GetComponent<ShotgunBullet>();
        Bull4.started(new Vector3(0, 15, 0));
        GameObject bull5 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull5 = bull5.GetComponent<ShotgunBullet>();
        Bull5.started(new Vector3(0, 20, 0));
        GameObject bull6 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull6 = bull6.GetComponent<ShotgunBullet>();
        Bull6.started(new Vector3(0, -5, 0));
        GameObject bull7 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull7 = bull7.GetComponent<ShotgunBullet>();
        Bull7.started(new Vector3(0, -10, 0));
        GameObject bull8 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull8 = bull8.GetComponent<ShotgunBullet>();
        Bull8.started(new Vector3(0, -15, 0));
        GameObject bull9 = (Instantiate(Bullet, (transform.position + transform.forward), rotation)) as GameObject;
        ShotgunBullet Bull9 = bull9.GetComponent<ShotgunBullet>();
        Bull9.started(new Vector3(0, -20, 0));
        //Quaternion.Euler(0, -45, 0) * vector;
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
