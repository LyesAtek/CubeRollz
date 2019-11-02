using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GoToGameOverScene : MonoBehaviour {
	private bool isDead;
	private float gameOverCounter;
	public float gameOverCounterInitial;
	private GameObject player;
	private GameObject gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("EG_GameController");
		player = GameObject.Find ("Player");
		gameOverCounter = gameOverCounterInitial;
		isDead = LifeManager.GetIsDead ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (LifeManager.GetIsDead ()) {
			gameOver ();
			gameOverCounter -= Time.deltaTime;
		}
		if (gameOverCounter <= 0) {
			//SceneManager.LoadScene ("GameOver");
			Destroy (GetComponent<Animator>());
			gameOverCounter = gameOverCounterInitial;

		}

	}

	public void restartGame(){

		Animator newAnim = gameObject.AddComponent<Animator> ();
		newAnim.enabled = true;
		GetComponent<Animator> ().runtimeAnimatorController = Resources.Load ("Animations/Player") as RuntimeAnimatorController;
		//gameObject.GetComponent<DestroyByContact> ().reinitializeCameraPosition ();
		gameController.GetComponent<GameController> ().StartGame ();
		GameObject.Find ("GameOverController").GetComponent<GameOverController> ().Start ();
		LifeManager.SetIsDead(false);
	}

	void gameOver(){
		//gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		//gameObject.GetComponent<MeshRenderer> ().enabled = false;
		//puddle.GetComponent<SpriteRenderer>().enabled = true;
	}
}
