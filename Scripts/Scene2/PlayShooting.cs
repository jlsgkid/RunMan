using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShooting : MonoBehaviour {

	Animator anim;
	public int damagePerShot = 10;

	//zan cun
	public float timeBetweenAttack = 0.2f;
	public float range = 100f;

	float timer = 0f;
	Ray shootRay;

	//public ParticleSystem shootEff;  //hit the dragon 
	LineRenderer shootLine;
	public AudioSource shootAudio;
	Light shootLight;
	public float effectDisplayTime = 0.2f;

	// Use this for initialization
	void Start () {
		//shootEff = GetComponent<ParticleSystem>();
		shootLine = GetComponent<LineRenderer>();
		shootAudio = GetComponent<AudioSource>();
		shootLight = GetComponent<Light>();
		//shootAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//if attack 
//		if(){
//
//		}
//		if(timer >= timeBetweenAttack * effectDisplayTime){
//			DisableEff();
//
//		}
	}

	public void Shoot(){
		Debug.Log ("This is Shoot");
		shootLight.enabled = true;
		timer = 0f;

		//shootEff.Stop();
		//shootEff.Simulate(0.1f);
		//shootEff.Play();
		shootLine.enabled = true;
		shootLine.SetPosition(0, transform.position);  // The first pos

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		// shexian jiance
		RaycastHit shootHit;
		if(Physics.Raycast(shootRay, out shootHit)){
			DragonHealth dh = shootHit.collider.GetComponent<DragonHealth>();
			if(dh!=null){
				dh.TakeDamage(damagePerShot);
				//dh.TakeDamage(damagePerShot, shootHit.point);

			}
			shootLine.SetPosition(1, shootHit.point);  // The second pos

		}else{
			shootLine.SetPosition(1, shootRay.origin + shootRay.direction * range);  // The second pos
		}
	}

	void DisableEff(){
		shootLine.enabled = false;
		shootLight.enabled = false;

	}

}
