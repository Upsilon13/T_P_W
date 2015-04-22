using UnityEngine;
using System.Collections;

public class EnemyCondition : MonoBehaviour {

    private int StartingHealth=100;
    private int CurrentHealth;
    private CapsuleCollider Caps;
    private float stun_duracion;
    private bool is_Stun;
    //private bool wield=true;
    ParticleSystem particle;
    public bool Dead;
    public bool hold;

    
    void Awake()
    {
        is_Stun = false;
        hold = false;
        //Dead = false;
        CurrentHealth = StartingHealth;
        particle = GetComponent<ParticleSystem>();
        //Caps = GetComponent<CapsuleCollider>();
        particle.Pause();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (CurrentHealth<=0)
        {
            Death();
        }
        if (is_Stun)
        {
            Stuned();
        }
        
	
	}

    public void TakeDamage(int dommage)
    {
        CurrentHealth = CurrentHealth - dommage;
    }

    //Not Complete
    void Death ()
    {
        Dead = true;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        GetComponent<BoxCollider>().enabled=false;
    }

    public void Stun()
    {
        is_Stun = true;
        stun_duracion = 0;
        
    }
    //Not Complete
    void Stuned()
    {
        if (stun_duracion < 5)
        {
            stun_duracion += Time.deltaTime;
            particle.Play();
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            is_Stun = false;
            particle.Play();
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    

}
