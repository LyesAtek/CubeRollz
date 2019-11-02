using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreditController : MonoBehaviour {
	public Text credits;
	// Use this for initialization
	void Start () {
		credits.text = CreditManager.GetCredits ().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		credits.text = CreditManager.GetCredits ().ToString();
	}
}
