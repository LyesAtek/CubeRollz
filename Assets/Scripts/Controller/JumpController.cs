using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){ 
		if (other.tag == "Jump") {
			other.gameObject.transform.GetChild (1).gameObject.SetActive (false);
			other.gameObject.transform.GetChild (2).gameObject.SetActive (true);
		}


	}
}
