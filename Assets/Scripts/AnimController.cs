using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

	private float rangeSecond = 0.5f;
	private float min = -1f;
	private float max = 0f;
	private float speed = 0.07f;
	private float second;
	private bool isStarted = false;

	void Start () {
		second = Random.Range (0, rangeSecond);
	}

	void FixedUpdate () {
		second -= Time.deltaTime;
		if (second <= 0 && !isStarted) {
			StartCoroutine (moveUp ());
			isStarted = true;
		}
	}

	IEnumerator moveUp(){
		while (transform.position.y < max) {
			transform.Translate (new Vector3 (0f, speed, 0f));
			yield return new WaitForFixedUpdate ();
		}
		StartCoroutine (moveDown ());
	}

	IEnumerator moveDown(){
		while (transform.position.y > min) {
			transform.Translate (new Vector3 (0f, -speed, 0f));
			yield return new WaitForFixedUpdate ();
		}
		StartCoroutine (moveUp ());
	}
}

