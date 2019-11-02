using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour {
	
	private static GameObject player;
	public static int indexPlayerSkin;

	void Start () {
		if (!PlayerPrefs.HasKey ("PlayerSkin")) {
			indexPlayerSkin = 0;
			PlayerPrefs.SetInt ("PlayerSkin", indexPlayerSkin);
		}
		player = GameObject.Find ("Player");
	}
		
	public static int getIndexPlayerSkin(){
		return PlayerPrefs.GetInt ("PlayerSkin");
	}

	public static void setIndexPlayerSkin(int value){
		indexPlayerSkin = value;
		PlayerPrefs.SetInt ("PlayerSkin", indexPlayerSkin);
	}

	public static void setPlayerSkin(GameObject skin){
		Destroy (player.transform.GetChild (0).gameObject);
		Instantiate (skin, player.transform);
		//player.GetComponent<Renderer> ().enabled = true;
	}
}
