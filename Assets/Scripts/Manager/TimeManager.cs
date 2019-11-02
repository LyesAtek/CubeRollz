using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
	private static int timer;
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("Timer")) {
			PlayerPrefs.SetInt ("Timer", 120);
		}
	}

	public static int GetTimer(){
		return PlayerPrefs.GetInt ("Timer");
	}

	public static void TakeTime(int value){
		timer = PlayerPrefs.GetInt ("Timer") -value;
		PlayerPrefs.SetInt ("Timer", timer);
	}

	public static void ReInitializeTimer(){
		PlayerPrefs.SetInt ("Timer", 120);
	}

}
