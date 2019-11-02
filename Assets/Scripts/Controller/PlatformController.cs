using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
	private float time = 0f;
	private float timeToDezoom = 23f;
	private Vector3 startPosition;
	private Vector3 endPosition;
	// Use this for initialization

	public bool moving = true; 

	void Start () {
		endPosition = new Vector3 (0f, 0f, -7f);
		startPosition = transform.position;
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!LifeManager.GetIsDead () && moving) {
			time += Time.deltaTime / timeToDezoom;
			transform.Translate (Vector3.back * timeToDezoom * (Time.deltaTime));
		}
	}
}
