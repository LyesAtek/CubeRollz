using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;public class ButtonAnimationClick : MonoBehaviour {
	private static bool isFinish = true;
	void Start () {
	}



	public static IEnumerator clickButton(GameObject button,float offsetWidth,float speedAnimation){
		if (isFinish) {
			isFinish = false;
			float startWidth;
			float endWidth;
			startWidth = button.GetComponent<RectTransform> ().rect.width;
			endWidth = startWidth + offsetWidth;

			while (button.GetComponent<RectTransform> ().rect.width <= endWidth) {
				button.GetComponent<RectTransform> ().sizeDelta = new Vector2 (button.GetComponent<RectTransform> ().rect.width + speedAnimation, button.GetComponent<RectTransform> ().rect.height);
				yield return new WaitForFixedUpdate ();
			}
			button.GetComponent<RectTransform> ().sizeDelta = new Vector2 (endWidth, button.GetComponent<RectTransform> ().rect.height);
			while (button.GetComponent<RectTransform> ().rect.width >= startWidth) {
				button.GetComponent<RectTransform> ().sizeDelta = new Vector2 (button.GetComponent<RectTransform> ().rect.width - speedAnimation, button.GetComponent<RectTransform> ().rect.height);
				yield return new WaitForFixedUpdate ();
			}
			button.GetComponent<RectTransform> ().sizeDelta = new Vector2 (startWidth, button.GetComponent<RectTransform> ().rect.height);
		

			isFinish = true;
	
		}
	}



}
