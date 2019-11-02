using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BestScoreController : MonoBehaviour {
	public Text bestScore;
	// Use this for initialization
	void Start () {
		bestScore.text = "High Score : "  + ScoreManager.GetBestScore ().ToString ();
	}

	// Update is called once per frame
	void Update () {

	}
}
