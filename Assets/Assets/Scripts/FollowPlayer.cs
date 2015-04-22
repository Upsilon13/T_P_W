using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	private Transform target;
	private float step = 0.05f;
    private EnemyCondition enemy;
    public bool attack;
    Rigidbody EnemyRigidbody;
    Vector3 vect;
    Animator anim;
    float y;

	// Use this for initialization
	void Start () 
    {
        y = transform.position.y;
        anim = GetComponent<Animator>();
        enemy = gameObject.GetComponent<EnemyCondition>();
		target = GameObject.Find("Player").transform;
        EnemyRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        attack = false;
        Turning();
		if (transform.position != target.position&& enemy.Dead==false&&Vector3.Distance(transform.position,target.position)>3)
        {
            vect= Vector3.MoveTowards (transform.position, target.position, step);
            vect.y = y;
            transform.position = vect;
            attack = true;
            
		}
        else
        {
            attack = false;
        }
	}
    void Turning()
    {
            Vector3 playerToMouse = target.position - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            EnemyRigidbody.MoveRotation(newRotation);
        
    }
}