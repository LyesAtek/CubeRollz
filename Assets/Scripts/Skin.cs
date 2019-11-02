using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour {
	private string name;
	private int price;
	private Sprite image;
	private GameObject skin;
	private bool isUnlocked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Skin(string name,int credit,Sprite image, GameObject skin, bool unlocked){
		this.name = name;
		this.price = credit;
		this.image = image;
		this.skin = skin;
		this.isUnlocked = unlocked;
	}

	public void SetPrice(int credit){
		this.price = credit;
	}

	public string GetName(){
		return name;
	}

	public int GetPrice(){
		return price;
	}

	public bool GetBool(){
		return isUnlocked;
	}
		
	public Sprite GetImage(){
		return image;
	}

	public GameObject GetSkin(){
		return skin;
	}

	public bool IsUnlocked(){
		return PlayerPrefs.HasKey (name) && PlayerPrefs.GetInt (name) == 1;
	}
}
