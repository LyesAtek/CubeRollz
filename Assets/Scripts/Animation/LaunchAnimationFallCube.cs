using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAnimationFallCube : MonoBehaviour {
	public float positionZ;
	private Animator anim;
	private bool isPlay;
	private GameObject player;
	// Use this for initialization
	void Start () {
		
		player = GameObject.Find ("Player");
		positionZ = player.GetComponent<PlayerController> ().GetPositionToLaunch () + 10;
		isPlay = false;
		anim = GetComponent<Animator> ();
		anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent == null) {
			if (transform.position.z <= (player.transform.position.z + positionZ) && !isPlay) {
				isPlay = true;
				anim.enabled = true;
			}
		}else if (transform.parent.transform.position.z <= (player.transform.position.z + positionZ) && !isPlay) {
			isPlay = true;
			anim.enabled = true;
		}
	}


}
