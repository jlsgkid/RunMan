using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s1_cameraContrl : MonoBehaviour {

	PlayerMove pm;
	public ParticleSystem fireParticle;

	// Use this for initialization
	void Start () {
		pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove> ();
	}
	
	// Update is called once per frame
	void Update () {
		Jump ();
	}

	void Jump(){
		RaycastHit shootHit;
		if(Physics.Raycast(transform.position, transform.forward, out shootHit)){
			
			if (shootHit.transform.tag == "JumpGaze") {
				Debug.Log ("jump111");
				pm.touchDir = TouchDir.Top;
			}
			if (shootHit.transform.tag == "Monster1") {
				Debug.Log ("Monster");
				GameObject.Instantiate (fireParticle, shootHit.point, Quaternion.identity);
				//fireParticle.transform.position = shootHit.point;
				fireParticle.Play ();
				Destroy (shootHit.transform.gameObject, 1.5f);
			}

			//Debug.Log ("jump222");
		}else{
			//shootLine.SetPosition(1, shootRay.origin + shootRay.direction * range);  // The second pos
		}
	}
}
