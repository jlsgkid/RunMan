using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour {
	
	Animator anim;
	public float timeBetweenAttack = 12f;
	public int attackDamage = 20;

	GameObject player;
	playSpwan ps ;
	bool ifAttackRange = true;
	float timer;
	//fire
	public ParticleSystem fireParticle;
	DragonHealth dragonLife;
	public int timerInt = 0;
	public AudioSource fireClip;
	public GameObject trig;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		ps = player.GetComponent<playSpwan>();
		dragonLife = GetComponent<DragonHealth>();
		//fireParticle = GetComponentInChildren<ParticleSystem>();  //fire system
		fireParticle.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		timerInt++;
		timer += Time.deltaTime;
		if(timer > timeBetweenAttack && ifAttackRange && (dragonLife.currentHealth > 0)){
			if(timerInt < 500){
				//anim.SetBool("Idle",false);
			}
			if (timerInt < 1000 && timerInt >600) {

				anim.SetBool("Fly Idle",true);
				trig.SetActive (false);
			} else if(timerInt >1000) {
				anim.SetBool("Fly Idle",false);
				Attack();
				trig.SetActive (true);
			}

		}
		if(ps.currentLife <= 0){
			anim.SetBool("win",true);
		}
		if(dragonLife.currentHealth < 60){
			// fire Attack

		}
	}

	void Attack(){
		timer = 0f;
		if(ps.currentLife > 0){
			anim.speed = 0.8f;
			anim.SetTrigger ("Fire Breath Attack");
			//GameObject.Instantiate (fireParticle, this.transform);
			Invoke("PlayFire",0.5f);
			fireClip.Play ();
			ps.TakeDamage(attackDamage);
		}
	}
	void PlayFire(){
		fireParticle.Play ();
	}
}
