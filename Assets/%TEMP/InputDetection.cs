using UnityEngine;
using System.Collections;

public class InputDetection : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("You pressed something -_-");
        }

    }
}
