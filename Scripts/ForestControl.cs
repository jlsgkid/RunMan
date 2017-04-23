using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestControl : MonoBehaviour {

	public GameObject[] forests;
	public GameObject curForest;
	[SerializeField]
	private WayPoints wayPoints;

	void Awake(){
		if (forests.Length > 0) {
			curForest = forests [0];
		}
		wayPoints = curForest.transform.Find("wayPoints").GetComponent<WayPoints>();
	}

	public Vector3 GetNextWayPoint(int index) {
		wayPoints = curForest.transform.Find("wayPoints").GetComponent<WayPoints>();
		return wayPoints.waypoints[index].position;
	}

	public int GetCurrentForestWPNum(){
		wayPoints = curForest.transform.Find("wayPoints").GetComponent<WayPoints>();
		return wayPoints.waypoints.Length;
	}

	public void ChangeForest(){
		for (int i = 0; i < forests.Length; i++) {
			if (curForest == forests [i]) {
				if (i < forests.Length - 1) {
					curForest = forests [i + 1];
					Destroy (GameObject.FindGameObjectWithTag (forests [i].tag), 2);
					break;
				}
			}
		}
	}



}
