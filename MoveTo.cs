using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	
	Transform goal;
	NavMeshAgent nav;
	Collider other;
	bool _isSeen;
	float fieldOfViewAngle = 110f;

	void Awake() {
		nav = GetComponent<NavMeshAgent> ();
		goal = GameObject.Find("Player").transform;
		_isSeen = (CalculatePathLength(goal.position) < 10f) && (InSight());
		if (_isSeen)
			nav.destination = goal.position;
	}
	
	void Update() {
		_isSeen = (CalculatePathLength(goal.position) < 10f) && (InSight());
		if (_isSeen)
			nav.destination = goal.position;
	}

	float CalculatePathLength (Vector3 targetPosition) {
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled)
			nav.CalculatePath(targetPosition, path);
		Vector3 [] allWayPoints = new Vector3[path.corners.Length + 2];
		allWayPoints[0] = transform.position;
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		for(int i = 0; i < path.corners.Length; i++)
			allWayPoints[i + 1] = path.corners[i];
		float pathLength = 0;
		for(int i = 0; i < allWayPoints.Length - 1; i++)
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		return pathLength;
	}

	bool InSight() {
		bool b = false;
		Vector3 direction = goal.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);
		if (angle < fieldOfViewAngle * 0.5f) 
			b = true;
		return b;
	}
}