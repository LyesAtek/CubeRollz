using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMoveManager : MonoBehaviour {
    private static int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static IEnumerator moveIn(GameObject go,string direction,float speedAnimation){
		count++;
		bool reachTarget = false;
		Vector3 endPosition;
		Vector3 startPosition;
		float offsetY = 1000f;
		float offsetX = 1000f;
		bool isFinish = false;
		switch (direction) {
		case "top": 
			endPosition = go.transform.localPosition;
			startPosition = new Vector3 (go.transform.localPosition.x, go.transform.localPosition.y + offsetY, go.transform.localPosition.z);
			go.transform.localPosition = startPosition;
			reachTarget = false;
			while (!reachTarget) {
				go.transform.Translate (0f, -speedAnimation, 0f);// = new Vector3 (transform.localPosition.x, transform.localPosition.y - speedAnimation, transform.localPosition.z);
				if (go.transform.localPosition.y <= endPosition.y) {
					go.transform.localPosition = endPosition;
					reachTarget = true;
				}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;
		case "bottom":
		    endPosition = go.transform.localPosition;
			startPosition = new Vector3 (go.transform.localPosition.x, go.transform.localPosition.y - offsetY, go.transform.localPosition.z);
			go.transform.localPosition = startPosition;
			reachTarget = false;
			while (!reachTarget) {
					go.transform.Translate (0f, speedAnimation, 0f);
					if (go.transform.localPosition.y >= endPosition.y) {
						go.transform.localPosition = endPosition;
						reachTarget = true;
					}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;
		case "left": 
			endPosition = go.transform.localPosition;
			startPosition = new Vector3 (go.transform.localPosition.x + offsetX, go.transform.localPosition.y, go.transform.localPosition.z);
			go.transform.localPosition = startPosition;
		    reachTarget = false;
			while (!reachTarget) {
				go.transform.Translate (-speedAnimation, 0f, 0f);// = new Vector3 (transform.localPosition.x, transform.localPosition.y - speedAnimation, transform.localPosition.z);
				if (go.transform.localPosition.x <= endPosition.x) {
					go.transform.localPosition = endPosition;
					reachTarget = true;
				}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;

		case "right":
			endPosition = go.transform.localPosition;
			startPosition = new Vector3 (go.transform.localPosition.x - offsetX, go.transform.localPosition.y, go.transform.localPosition.z);
			go.transform.localPosition = startPosition;
		    reachTarget = false;
			while (!reachTarget) {
					go.transform.Translate (speedAnimation, 0f, 0f);// = new Vector3 (transform.localPosition.x, transform.localPosition.y - speedAnimation, transform.localPosition.z);
					if (go.transform.localPosition.x >= endPosition.x) {
						go.transform.localPosition = endPosition;
						reachTarget = true;
					}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;
		
		default : 
			break;

		}

	}

	public  static IEnumerator moveOut(GameObject go,string direction,float speedAnimation){
		count++;
		bool reachTarget = false;
		Vector3 endPosition;
		Vector3 startPosition;
		float offsetY = 1000f;
		float offsetX = 1000f;
		switch (direction) {
		case "top": 
			
			startPosition = go.transform.localPosition;
			endPosition = new Vector3 (go.transform.localPosition.x, go.transform.localPosition.y + offsetY, go.transform.localPosition.z);
			reachTarget = false;
			while (!reachTarget) {
				go.transform.Translate (0f, speedAnimation, 0f);// = new Vector3 (transform.localPosition.x, transform.localPosition.y - speedAnimation, transform.localPosition.z);
				if (go.transform.localPosition.y >= endPosition.y) {
					go.transform.localPosition = endPosition;
					reachTarget = true;
				}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;
		case "bottom":
			startPosition= go.transform.localPosition;
			endPosition = new Vector3 (go.transform.localPosition.x, go.transform.localPosition.y - offsetY, go.transform.localPosition.z);
		    reachTarget = false;
			while (!reachTarget) {
				go.transform.Translate (0f, -speedAnimation, 0f);
				if (go.transform.localPosition.y <= endPosition.y) {
					go.transform.localPosition = endPosition;
					reachTarget = true;
				}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;
		case "left": 
			startPosition = go.transform.localPosition;
			endPosition = new Vector3 (go.transform.localPosition.x + offsetX, go.transform.localPosition.y, go.transform.localPosition.z);
		    reachTarget = false;
			while (!reachTarget) {
				go.transform.Translate (speedAnimation, 0f, 0f);// = new Vector3 (transform.localPosition.x, transform.localPosition.y - speedAnimation, transform.localPosition.z);
				if (go.transform.localPosition.x >= endPosition.x) {
					go.transform.localPosition = endPosition;
					reachTarget = true;
				}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;

		case "right":
			startPosition = go.transform.localPosition;
			endPosition = new Vector3 (go.transform.localPosition.x - offsetX, go.transform.localPosition.y, go.transform.localPosition.z);
		    reachTarget = false;
			while (!reachTarget) {
				go.transform.Translate (-speedAnimation, 0f, 0f);// = new Vector3 (transform.localPosition.x, transform.localPosition.y - speedAnimation, transform.localPosition.z);
				if (go.transform.localPosition.x <= endPosition.x) {
					go.transform.localPosition = endPosition;
					reachTarget = true;
				}
				yield return new WaitForFixedUpdate ();
			} 
			count--;
			break;

		default : 
			break;

		}

	}

	public static bool isFinish(){
		return count == 0;
	}







}
