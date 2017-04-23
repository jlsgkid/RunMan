using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public float xSpeed = 0.1f;
	public float ySpeed = 5f;
	// Use this for initialization
	private int i = 0;
	public float timer = 0;
	void Start () {
		//Invoke ("CMove", 3);
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1 / ySpeed * Time.deltaTime;
		if (timer > 1) {
			this.transform.position += new Vector3 (0, ySpeed, 0);
		}
		timer = 0;
//		if (i < 10) {
//			this.transform.position += new Vector3 (0, ySpeed, 0);
//		}
//		i++;
//		if (i > 15&& i<25) {
//			this.transform.position -= new Vector3 (0, Time.deltaTime * ySpeed, 0);
//		}
	}

}
