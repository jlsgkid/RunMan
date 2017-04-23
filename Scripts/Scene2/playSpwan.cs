using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playSpwan : MonoBehaviour {

	Animator anim;
	public float life = 100f;
	public float currentLife = 0f;
	//image
	public Image damageImage;
	public Slider lifeSlider;
	//public AudioSource playerAduio;
	bool isDead;
	bool damaged = false;

	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
		currentLife = life;
		//playerAduio = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(damaged){
			damageImage.color = Color.red;
		}else{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, 3 * Time.deltaTime);
		}
		damaged = false;
	}
	public void Attack(){
		//anim.SetBool ("attack", true);
	}
	public void TakeDamage(int damage){
		damaged = true;
		currentLife -= damage;
		lifeSlider.value = currentLife;
		//playerAduio.Play();
		if(currentLife <= 0 && isDead){
			Death();
		}
	}

	void Death(){
		isDead = true;
		anim.SetTrigger("Die");
		//playerAduio.clip = deathClip;// zanding
		//playerAduio.Play();
	}
}
