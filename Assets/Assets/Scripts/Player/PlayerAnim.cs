using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    Animator anim;
    PlayerController player;

	void Awake () {
        player= GetComponentInParent<PlayerController>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.walk())
        {
            anim.SetBool("Iswalking", true);
        }
	
	}
}
