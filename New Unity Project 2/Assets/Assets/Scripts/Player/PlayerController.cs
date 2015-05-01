using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    private PlayerCondition PlC;
    bool d = true;
    public bool is_walking;
    protected bool armed;
    bool attackb;
    float timer;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        attackb = false;
        timer = 5;
        is_walking = false;
        armed = false;
        floorMask = LayerMask.GetMask("Floor");
        PlC = gameObject.GetComponent<PlayerCondition>();
        // Set up references.

        playerRigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {

        if (PlC.isDead())
        {
            d = true;
        }
        else
        {
            d = false;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (armed == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Mouse0");
                attack();
            }
        }
    }

    void FixedUpdate()
    {
        attackb = false;
        if (d)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            Move(moveHorizontal, moveVertical);
            Turning();
            if (moveHorizontal != 0 || moveVertical != 0)
                is_walking = true;
            else
                is_walking = false;
        }

    }



    void Move(float moveHorizontal, float moveVertical)
    {
        movement.Set(moveHorizontal, 0f, moveVertical);
        movement = movement.normalized * speed*Time.deltaTime ;
        playerRigidbody.MovePosition(transform.position + movement);
        /*playerRigidbody.AddForce(movement * 10);
        if (moveHorizontal==0&&moveVertical==0)
        {
            playerRigidbody.velocity = new Vector3(0,0,0);   
        }*/
        
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public bool walk()
    {
        return is_walking;
    }

    private void attack()
    {
        if (timer>0.25&&armed==false)
        {
            attackb = true;
            Debug.Log("DIE!");
            timer = 0;
        }
        
    }

    public bool IsAttacking()
    {
        return attackb;
    }

    public void SetArmed(bool b)
    {
        armed = b;
    }

    //Not Complete

}
