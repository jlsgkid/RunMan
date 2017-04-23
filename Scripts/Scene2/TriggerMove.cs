using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMove : MonoBehaviour {

	public GameObject target;
	public float smoothing=5f;
	[SerializeField]
	private Vector3 offset ;
	// Use this for initialization
	void Start () {
		offset = this.transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetCampos = target.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCampos, smoothing * Time.deltaTime);
	}
}
