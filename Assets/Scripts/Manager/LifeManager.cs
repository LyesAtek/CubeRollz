using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

	public static bool isDead;
	public static int lifeCount;
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("LifeCount")) {
			PlayerPrefs.SetInt ("LifeCount", 10);
		}
		//PlayerPrefs.SetInt ("LifeCount", 1);
		isDead = false;
	}

	public static int GetLifeCount(){
		return PlayerPrefs.GetInt ("LifeCount");
	}

	public static void TakeLife(int value){
		lifeCount = PlayerPrefs.GetInt ("LifeCount") -value;
		PlayerPrefs.SetInt ("LifeCount", lifeCount);

	}

	public static void AddLife(int value){
		lifeCount = PlayerPrefs.GetInt ("LifeCount") +value;
		PlayerPrefs.SetInt ("LifeCount", lifeCount);
	}

	public static bool GetIsDead(){
		return isDead;
	}

	public static void SetIsDead(bool value){
		isDead = value;
	}
}
