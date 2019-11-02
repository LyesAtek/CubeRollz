using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopController : MonoBehaviour {

	private List<Skin> skins;
	private ShopManager shopManager;
	private Sprite lockedSkinImage;
	public Text creditLabel;
	void Start () {
		shopManager = Object.FindObjectOfType<ShopManager> ();
		shopManager.Start ();
		lockedSkinImage = Resources.Load ("UI/locked_skin", typeof(Sprite)) as Sprite;
	
		initializeSkins();
	}


	void initializeSkins(){
		skins = shopManager.getSkins ();
		Object itemPrefab = Resources.Load ("Prefabs/ShopItem"); 
		for (int i = 0; i < skins.Count; i++) {
			Skin skin = skins [i];
			bool isSkinUnlocked = skin.IsUnlocked ();
			GameObject item = Instantiate (itemPrefab, transform) as GameObject;
			item.name = skin.GetName ();
			item.GetComponent<Button>().onClick.AddListener(onSkinSelected);
			item.GetComponentInChildren<Image> ().sprite = isSkinUnlocked ? skin.GetImage () : lockedSkinImage;
			item.GetComponentInChildren<Text> ().text = isSkinUnlocked ? "" : skins[i].GetPrice().ToString();
			item.transform.Find ("Data").transform.Find ("Coin").gameObject.SetActive (!isSkinUnlocked);
		}
	}

	public bool isSkinUnlocked(int index){
		string skinName = skins [index].GetName ();
		if (PlayerPrefs.GetInt (skinName) == 0) {
			return false;
		}
			return true;
	}

	public void unlockSkin(int index){
		string skinName = skins [index].GetName ();
		PlayerPrefs.SetInt (skinName, 1);
	}

	public void onSkinSelected(){
		GameObject currentObject = EventSystem.current.currentSelectedGameObject;
		string nameOfButton = currentObject.name.Remove (0, 4);
		int selectedIndex = int.Parse (nameOfButton) - 1;
		Skin skin = skins [selectedIndex];
		if (!shopManager.skinIsUnlock (selectedIndex)) {
			bool hasPlayerEnoughMoney = skin.GetPrice () <= CreditManager.GetCredits ();
			if (hasPlayerEnoughMoney) {
				buySkin (selectedIndex);
			}
		} else {
			setSkin (selectedIndex);
			SoundManager.Instance.playButtonClickAudio ();
		}
	}

	public void setSkin(int selectedIndex){
		PlayerSkin.setIndexPlayerSkin (selectedIndex);
		GameObject skin = skins [selectedIndex].GetSkin ();
		PlayerSkin.setPlayerSkin (skin);
		refreshShop ();
	}

	public void buySkin(int selectedIndex){
		CreditManager.SetCredits (-skins [selectedIndex].GetPrice ());
		creditLabel.text = CreditManager.GetCredits ().ToString();
		shopManager.unlockSkin (selectedIndex);
		setSkin (selectedIndex);
	}

	public void refreshShop(){
		int i = 0;
		foreach (Transform child in transform)
		{
			bool isSkinUnlocked = skins [i].IsUnlocked ();
			child.Find ("Image").gameObject.GetComponent<Image> ().sprite = isSkinUnlocked ? skins[i].GetImage () : lockedSkinImage;
			child.Find ("Data").transform.Find ("Price").gameObject.GetComponent<Text>().text = isSkinUnlocked ? "" : skins [i].GetPrice ().ToString ();
			child.transform.Find ("Data").transform.Find ("Coin").gameObject.SetActive (!isSkinUnlocked);
			i++;
		}
	}
}
