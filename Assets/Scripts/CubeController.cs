using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	public float rangeSecond;
	public Animator anim;
	public Animator anim2;
	public Animator anim3;
	public Animator anim4;
	public Animator anim5;
	private float second;
	private float offset = 0.4f;
	void Start () {
		second = Random.Range (0, rangeSecond);
		anim.enabled = false;
		anim2.enabled = false;
		anim3.enabled = false;
		anim4.enabled = false;
		anim5.enabled = false;
	}

	void FixedUpdate () {
		second -= Time.deltaTime;
		if (second <= 0) {
			
			anim.enabled = true;
			if (second <= -offset ) {
				anim2.enabled = true;
			}
			if (second <= -(offset * 2) ) {
				anim3.enabled = true;
			}
			if (second <= -(offset * 3) ) {
				anim4.enabled = true;
			}
			if (second <= -(offset * 4)) {
				anim5.enabled = true;
			}
		}

	}

}
