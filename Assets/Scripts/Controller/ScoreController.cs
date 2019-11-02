using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreController : MonoBehaviour {
	public Text score;
	// Use this for initialization
	void Start () {
		score.text = ScoreManager.GetScore ().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		score.text = ScoreManager.GetScore ().ToString();
	}
}
