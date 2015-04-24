using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    Transform goal;
    SeenByEnemy r;

    void Awake()
    {
        r = GetComponent<SeenByEnemy>();
        if (r._isSeen)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            goal = GameObject.Find("Player").transform;
            agent.destination = goal.position;
        }
    }

    void Update()
    {
        if (r._isSeen)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            goal = GameObject.Find("Player").transform;
            agent.destination = goal.position;
        }
    }
}
