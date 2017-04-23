using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShoot : MonoBehaviour {

	LineRenderer shootLine;
	Light shootLight;
	public GameObject vr_camera;
	float timer = 0f;
	public float effSpeed = 5f;
	private ParticleSystem shootEff;
	//private GameObject player;
	private Animator p_anim;
	public AudioSource shootAudio;
	public DragonHealth dh;

	// Use this for initialization
	void Start () {
		shootLine = GetComponent<LineRenderer>();
		shootLight = GetComponent<Light>();
		shootEff = GameObject.Find ("PS").GetComponent<ParticleSystem>(); 
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		p_anim = player.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//if attack 
		//		if(){
		//
		//		}

	}

	public void c_Shoot(){

		if(!shootAudio.isPlaying){
			shootAudio.Play ();
		}
		p_anim.speed = 1f;
		p_anim.SetBool ("MagicAttack01", true);
		shootLine.SetPosition(0, vr_camera.transform.position);  // The first pos
		shootLine.enabled = true;
		shootLight.enabled = true;
		RaycastHit shootHit;
		if(Physics.Raycast(transform.position, transform.forward, out shootHit)){
//			DragonHealth dh = shootHit.collider.GetComponent<DragonHealth>();
//			if(dh!=null){
//				dh.TakeDamage(10);
//				//dh.TakeDamage(damagePerShot, shootHit.point);
//				Debug.Log("Attack Dragon");
//
//			}
			dh.TakeDamage(10);
			shootLine.SetPosition(1, shootHit.point);  // The second pos
			//GameObject.Instantiate(shootEff,shootHit.transform);
			shootEff.transform.position = shootHit.point;
			shootEff.Play ();

		}else{
			//shootLine.SetPosition(1, shootRay.origin + shootRay.direction * range);  // The second pos
		}

		Invoke ("DisableEff", 0.5f);

	}
	void DisableEff(){
		shootLine.enabled = false;
		shootLight.enabled = false;
		p_anim.SetBool ("MagicAttack01", false);
	}
}
