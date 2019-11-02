using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DestroyByContact : MonoBehaviour {
	public Text credit;

	private bool isDead = false;
	private bool particleDeathPlayed = false;
	public float deathCount;
	private float deathCounter;
	public Camera mainCamera;
	public bool cameraDezoomed = false;
	public Vector3 startPosition;
	public Vector3 endPosition;
	private float time = 0f;
	private float timeToDezoom = 1f;
	public ParticleSystem particleDestroyPlayer;



	void Start () {

		deathCounter = deathCount;
	
	
	
	}


	void OnTriggerEnter(Collider other) {
		if((other.tag == "Finish" || other.tag == "GameOver") && !LifeManager.GetIsDead() )
		{
			SoundManager.Instance.playExplosionAudio ();
			PlayerPrefs.SetString ("Date", System.DateTime.Now.ToString());

			isDead = true;
			mainCamera.GetComponent<CameraController> ().Dezoom ();
			StartParticleDestroyPlayer ();

			LifeManager.SetIsDead (true);
			PlayerPrefs.SetInt ("CurrentScore", ScoreManager.GetScore());
			CreditManager.SetCredits (ScoreManager.GetScore ());

			credit.text = CreditManager.GetCredits ().ToString();



			LifeManager.TakeLife (1);
		}
	}



	public void StartParticleDestroyPlayer(){

		transform.GetChild(0).gameObject.SetActive(false);
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		Vector3 positionPlayer = new Vector3 (gameObject.transform.position.x - 1f, gameObject.transform.position.y, gameObject.transform.position.z);
		ParticleSystem particle = Instantiate (particleDestroyPlayer, positionPlayer,particleDestroyPlayer.transform.rotation);
		particle.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
	//	particle.GetComponent<Renderer> ().material = transform.GetChild(0).GetComponent<Renderer> ().material;
		particle.Play ();
	}



}
