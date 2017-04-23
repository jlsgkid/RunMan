using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonHealth : MonoBehaviour {

	public int currentHealth = 100;
	Animator anim;
	public AudioSource dAudio;   //get damage sound
	bool isDead;
	public Slider lifeSlider;

	// Use this for initialization
	void Awake () {
		currentHealth = 100;
		//dAudio = GetComponent<AudioSource>();
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//void TakeDamage(int damage, Vector3 hitPoint);
	public void TakeDamage(int damage){
		Debug.Log ("Dragon Damage");
		if (isDead)
			return;
		currentHealth -= damage;
		lifeSlider.value = currentHealth;
		dAudio.Play ();
		if (currentHealth <= 0) {
			Death (); 
		}
	}
	
	void Death(){
		isDead = true;
		anim.SetTrigger("Dead");
		//dAudio.clip = deathClip ; //siwang sound
		dAudio.Play();
	}
}
