using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DynamicGridManager : MonoBehaviour {

	public int col;
	public int row;
	public GameObject button; 
	//private ShopManager shopManager;
	private ShopManager shopManager;
	private Object[] skills;
	private Object[] textureSkills;
	private RectTransform parent ;
	private GridLayoutGroup grid;

	private Sprite textureButton;

	private List<Skin> skins ;
	private SoundManager soundManager;
	// Use this for initialization
	void Start () {
		shopManager = gameObject.AddComponent (typeof(ShopManager)) as ShopManager;
		shopManager.initializeResources ();
		shopManager.initializeComponements ();
		initializeComponement ();
		getTextureSkills ();
		createDynamicGrid ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initializeComponement(){
		skills = shopManager.getSkinsPlayer ();
		skins = shopManager.getSkins ();

		parent = gameObject.GetComponent<RectTransform> ();
		grid = gameObject.GetComponent<GridLayoutGroup> ();

	}

	void createDynamicGrid(){

		for (int i = 0; i < skills.Length; i++) {
			textureButton = (Sprite)textureSkills[i];
			Button skillButton = Instantiate (button).gameObject.GetComponent<Button>();
			skillButton.transform.SetParent( parent.transform);


			skillButton.GetComponent<Image> ().sprite = textureButton; 
			skillButton.name = skins [i].GetName();
			skillButton.onClick.AddListener(onClickSkinButton);

			if (!shopManager.skinIsUnlock (i)) {
				skillButton.GetComponentInChildren<Text> ().text = skins [i].GetPrice ().ToString ();
			} else {
				skillButton.GetComponentInChildren<Text> ().text = " ";
			}
			if (i == PlayerSkin.getIndexPlayerSkin()) {
				skillButton.transform.GetChild (1).gameObject.SetActive (true);
			}

		}
		grid.cellSize = new Vector2 (parent.rect.width / col, parent.rect.height / row);
	}

	void refreshDynamicGrid(){
		int numberOfChildren = parent.transform.childCount;
		for (int i = 0; i < numberOfChildren; i++) {

			if (!shopManager.skinIsUnlock (i)) {
				parent.transform.GetChild(i).gameObject.GetComponentInChildren<Text> ().text = skins [i].GetPrice ().ToString ();
			} else {
				parent.transform.GetChild(i).gameObject.GetComponentInChildren<Text> ().text = " ";
			}
			if (i == PlayerSkin.getIndexPlayerSkin ()) {
				parent.transform.GetChild (i).gameObject.transform.GetChild (1).gameObject.SetActive (true);
			} else {
				parent.transform.GetChild (i).gameObject.transform.GetChild (1).gameObject.SetActive (false);
			}

		}
	}

	void getTextureSkills(){
		textureSkills = Resources.LoadAll("Images", typeof(Sprite));
	
	}

	void onClickSkinButton(){
		GameObject currentObject = EventSystem.current.currentSelectedGameObject;
		string nameOfButton = currentObject.name.Remove (0, 4);
		int indexOfSkin = int.Parse (nameOfButton) - 1;
		Skin skin = skins [indexOfSkin];
		if (!shopManager.skinIsUnlock (indexOfSkin)) {
			buySkin (skin.GetPrice (),indexOfSkin);
		} else {
			PlayerSkin.setIndexPlayerSkin (indexOfSkin);
			refreshDynamicGrid ();
		}

	}

	void buySkin(int price, int indexOfSkin){
		Debug.Log ("Price : " + price + " credits : " + CreditManager.GetCredits ());
		if (price <= CreditManager.GetCredits ()) {
			CreditManager.SetCredits (-price);
			PlayerSkin.setIndexPlayerSkin (indexOfSkin);
			shopManager.unlockSkin (indexOfSkin);
			refreshDynamicGrid ();
		} else {
			//soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
			//soundManager.playErrorSound ();
		}
	}

}
	