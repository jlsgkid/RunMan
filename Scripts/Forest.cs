using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

    public float startLength = -90;
    public float minLength = 10;
    public float maxLength = 100;

    public Obstacles[] obstacles;

    private Transform player;
    public WayPoints waypoints;
    private int targetWayPointIndex = 0;

    void Awake() {
        GameObject playerGo = GameObject.FindGameObjectWithTag(Tags.player);
        if (playerGo != null) {
            player = playerGo.transform;
        }
        waypoints = transform.Find("wayPoints").GetComponent<WayPoints>();
        targetWayPointIndex = waypoints.waypoints.Length - 2;
    }

    void Start() {
        GenerateObstacles();
    }
		
    public Vector3 GetNextWayPoint() {
		while (true) {
			Debug.Log ("player.position.z :" + player.position.z);
           // if (waypoints.waypoints[targetWayPointIndex].position.z - player.position.z < 0.5f) {
			if(Vector3.Distance(waypoints.waypoints[targetWayPointIndex].position, player.position) < 1f){


                targetWayPointIndex--;

                if (targetWayPointIndex < 0) {
                    targetWayPointIndex = 0;
                    //Destroy(this.gameObject);
                    //Camera.main.SendMessage("UpdateForest", SendMessageOptions.DontRequireReceiver);
					GameObject forest2 = GameObject.FindGameObjectWithTag("Forest2");
					waypoints = forest2.transform.Find("wayPoints").GetComponent<WayPoints>();
                    return waypoints.waypoints[0].position;
                }
            } else {
                return waypoints.waypoints[targetWayPointIndex].position;
            }
        }
    }

    void GenerateObstacles() {
        float z = startLength;
        while (true) {
            float length = Random.RandomRange(minLength, maxLength);
            z += length;
			//Debug.Log ("z:"+z);
            if (z > 180) break;
            Vector3 waypoint = GetWayPoint(z);
            GenerateObstacles(waypoint);
        }
    }

    void GenerateObstacles(Vector3 position) {
        int index = Random.Range(0, obstacles.Length);
        Obstacles obs = (GameObject.Instantiate(obstacles[index]) as Obstacles);
        obs.InitSelf(position,this.transform);
		//Debug.Log (position.x + ":" + position.y + ":" + position.z);
    }

    Vector3 GetWayPoint(float z) {
        Transform[] wps = waypoints.waypoints;
        int index = GetIndex(z);
        return Vector3.Lerp(wps[index + 1].position, wps[index].position, (z + wps[wps.Length-1].position.z - wps[index + 1].position.z) / (wps[index].position.z - wps[index + 1].position.z));
    }

    int GetIndex(float z) {
        Transform[] wps = waypoints.waypoints;
		Debug.Log (wps.Length);
        int index = 0;
        for (int i = 0; i < wps.Length-2; i++) {
			//Debug.Log ("point" + i + "---" + wps [i].localPosition.z);
           // if (wps[i].position.z - startZ >= z) {
			if (z >= wps[i].localPosition.z && z <= wps[i+1].localPosition.z ) {
                index = i;
				Debug.Log ("point:" + index);
				break;
            } 
        }
        return index;
    }


}
