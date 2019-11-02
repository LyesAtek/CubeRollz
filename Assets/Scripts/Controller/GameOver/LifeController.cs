using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {
	public Text lifeCount;
	// Use this for initialization
	void Start () {
		lifeCount.text = LifeManager.GetLifeCount ().ToString ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		lifeCount.text = LifeManager.GetLifeCount ().ToString ();
	}


}
