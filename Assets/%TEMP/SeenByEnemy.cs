using UnityEngine;
using System.Collections;

public class SeenByEnemy : MonoBehaviour {

    GameObject EnemySight;
    public bool _isSeen = false;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            _isSeen = true;
    }

    void Start()
    {
        GameObject EnemySight = GameObject.Find("EnemySight");
        EnemySight.transform.position = gameObject.transform.position;
        EnemySight.transform.forward = gameObject.transform.forward;
        EnemySight.transform.rotation = gameObject.transform.rotation;
    }

    void Update()
    {
    }
    public void smg ()
    {

    }
}
