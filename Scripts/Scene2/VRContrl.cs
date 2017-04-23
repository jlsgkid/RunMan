using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRContrl : MonoBehaviour {

	public GameObject player;
	PlayerMove pm;
	// Use this for initialization
	public ParticleSystem fireParticle;
	//bool jumpFlg = false;
	void Start () {
		pm = player.GetComponent<PlayerMove> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnShixianIn(){
		if (GameController.gameState == GameState.Playing ) {
			//Debug.Log ("Jumping");
			pm.touchDir = TouchDir.Top;
		}

	}
	public void OnShixianExit(){
		if (GameController.gameState == GameState.Playing) {
			pm.touchDir = TouchDir.None;
		}
	}

	public void OnAttack(){
		Debug.Log ("On Attack");
		fireParticle.transform.position = this.transform.position;
		fireParticle.Play ();
		Destroy (this, 2);
	}
}
