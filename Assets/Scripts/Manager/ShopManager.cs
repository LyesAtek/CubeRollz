using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
	public static ShopManager Instance;

	private  Object[] playerSkins;
	private  Object[] images;
	private bool skinsBoolean;
	private List<Skin> skins;
	private  string skinName;
	public void Awake(){
		Instance = this;

	}
	public void Start () {
		if (skins != null && skins.Count > 0) {
			return;
		}
	
		initializeResources ();
		initializeComponements ();
		//reinitializePlayerPref ();
	}

	public void initializeResources(){
		playerSkins = Resources.LoadAll("Skins", typeof(GameObject));

		images = Resources.LoadAll("Images", typeof(Sprite));

	}

	public void initializeComponements() {
		skins = new List<Skin>();
		for (int i = 0; i < images.Length; i++) {
			skinName = playerSkins [i].name.Remove (playerSkins [i].name.IndexOf ('_'));
			if (!PlayerPrefs.HasKey (skinName)) {
				PlayerPrefs.SetInt (skinName, 0);
			}


			/////////////////////////
			//CreditManager.SetCredits (100);
			//PlayerPrefs.SetInt (skinName, 0); 
			/////////////////////////



			if (i == 0) {
				PlayerPrefs.SetInt (skinName, 1);
			}
			int price = int.Parse (playerSkins [i].name.Remove (0, playerSkins [i].name.IndexOf ('_') + 1));
			Skin skin = new Skin (skinName, price, images [i] as Sprite, playerSkins [i] as GameObject, skinIsUnlock (i));
			skins.Add (skin);
		}
	}

	public void reinitializePlayerPref(){
		for (int i = 0; i < playerSkins.Length; i++) {
			skinName = playerSkins [i].name.Remove(playerSkins [i].name.IndexOf('_'));

			if (PlayerPrefs.HasKey (skinName)) {
				PlayerPrefs.DeleteKey (skinName);
			}
		}
	}
	public  bool skinIsUnlock(int index){
		skinName = playerSkins [index].name.Remove (playerSkins [index].name.IndexOf ('_'));
		if (PlayerPrefs.GetInt (skinName) == 0) {
			return false;
		}
		return true;
	}

	public  void unlockSkin(int index){
		skinName = playerSkins [index].name.Remove (playerSkins [index].name.IndexOf ('_'));
		PlayerPrefs.SetInt (skinName, 1);
		SoundManager.Instance.playCashAudio ();
	}

	public  Object[] getSkinsPlayer(){	
		return playerSkins;
	}

	public List<Skin> getSkins(){
		return skins;
	}

	public Skin getSkin(int index){
		return skins [index];
	}
}
