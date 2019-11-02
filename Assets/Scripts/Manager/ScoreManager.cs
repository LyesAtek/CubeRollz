using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public static int score;
	// Use this for initialization
	void Start () {
		score = 0;
		if (!PlayerPrefs.HasKey ("Score")) {
			PlayerPrefs.SetInt ("Score", 0);
		}
		if (PlayerPrefs.HasKey ("BestScore")) {
			PlayerPrefs.SetInt ("BestScore", 0);
		}
	}
	




	public static void SetScore(int value){
		PlayerPrefs.SetInt ("Score", value);
	}

	public static void IncrementScore(){
		PlayerPrefs.SetInt ("Score", ScoreManager.GetScore() + 1);
	}

	public static void SetBestScore(int value){
		score = value;
		if (score > PlayerPrefs.GetInt ("BestScore")) {
			PlayerPrefs.SetInt ("BestScore", value);
		}
	}


	public static int GetScore(){
		return PlayerPrefs.GetInt ("Score");
	}

	public static int GetBestScore(){
		return PlayerPrefs.GetInt ("BestScore");
	}
}
