using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour {
	public static int credits;
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("Credit")) {
			PlayerPrefs.SetInt ("Credit", 0);
		}
		credits = PlayerPrefs.GetInt ("Credit");

	}


	public static int GetCredits ()
	{
		return PlayerPrefs.GetInt ("Credit");
	}

	public static void SetCredits (int value)
	{
		credits += value;
	    PlayerPrefs.SetInt ("Credit",credits);
	}



}
