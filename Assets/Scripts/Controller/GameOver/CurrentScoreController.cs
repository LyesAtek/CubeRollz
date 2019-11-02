using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentScoreController : MonoBehaviour {
	public Text currentScore;
	// Use this for initialization
	void Start () {
		currentScore.text = PlayerPrefs.GetInt ("CurrentScore").ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
