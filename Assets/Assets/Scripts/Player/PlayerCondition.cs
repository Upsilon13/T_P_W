﻿using UnityEngine;
using System.Collections;

public class PlayerCondition : MonoBehaviour {


    public bool Dead;
    int CurrentHealth = 1;

	// Use this for initialization
	void Awake () {
       // Dead = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (CurrentHealth<=0)
        {
            Death();
        }
        
	
	}

    void Death()
    {
        Dead = true;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        GetComponent<BoxCollider>().enabled = false;
    }

    public void TakeDamage(int dommage)
    {
        CurrentHealth = CurrentHealth - dommage;
    }

    public bool isDead()
    {
        if (CurrentHealth < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
