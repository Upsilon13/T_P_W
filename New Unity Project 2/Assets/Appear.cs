using UnityEngine;
using System.Collections;

public class Appear : MonoBehaviour {

    PlayerCondition stat;

    void Awake()
    {
        stat = GetComponentInParent<PlayerCondition>();
    }

    void LateUpdate()
    {
        if (stat.isDead() == false)
            gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}
