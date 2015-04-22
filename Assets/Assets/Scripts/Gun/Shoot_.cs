 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {
    
    public Transform Spawn;
    public GameObject Bullet;
    public float fireRate;
    private float NextShot = 0.0f;

	void Update () {
        if (Input.GetKey(KeyCode.Mouse0)&&NextShot<Time.time)
        {
            Instantiate(Bullet, Spawn.position, Spawn.rotation);
            NextShot = Time.time + fireRate;
            
        } 
	
	}
}
