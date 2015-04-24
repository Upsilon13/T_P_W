using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    GameObject Player;
	void Start () {
        Player = GameObject.FindWithTag("Player");
	}
	
	
	void Update () {
        transform.position = Player.transform.position+ new Vector3(0f,8f,0);
	}
}
