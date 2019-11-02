using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayerController : MonoBehaviour {
	private const float GROUND_Y = 0;
	private Rigidbody myRigidbody;
	public float moveHorizontal;
	private Vector3 startPosition;
	private Vector3 endPosition;
	public bool isMoving;
	public float moveVertical;
	public Text currentScoreText;
	public Button soundButton;
	public Text creditLbl;
	public GameController gameController;
	public int jumpFrames;
	public float jumpImpulsion;
	Vector3 moveDirection = Vector3.right;
	public BoxCollider jumpCollider;
	public BoxCollider basicCollider;
	Vector3 pivot;

	private int score;
	private int index;
	private int positionPlayer;
	public bool isJump;
	public float speedPlayer;
	private float currentSpeed;
	public float addAcceleration;
	public int firstStep;
	public int secondStep;
	public bool hasAccelerated;
	private int stateStep;
	public int initialPositionToLaunch;
	public int positionToLaunchAnimationFirstStep;
	public int positionToLaunchAnimationSecondStep;

	private bool isDead;
	public Camera mainCamera;
	public Text credit;
	public ParticleSystem particleDestroyPlayer;
	private Rotate rotateCtrl;
	public void Start () {

	

		rotateCtrl = GetComponent<Rotate> ();
		bool soundOn = SoundManager.Instance.getState ();
		setSoundState (soundOn);
		gameController = GameObject.Find ("EG_GameController").GetComponent<GameController> ();
		StartGame ();
	}
		

	public void StartGame(){
		creditLbl.text = CreditManager.GetCredits ().ToString ();
		transform.GetChild (0).gameObject.SetActive(true);
		stateStep = 0;
		currentSpeed = speedPlayer;
		SoundManager.Instance.playGameMusic ();
		positionPlayer = 0;
		transform.localPosition = new Vector3(0f, 2.36f, -4f);
		transform.rotation = Quaternion.identity;
		//transform.rotation = new Quaternion(0f, 0f, 1f, 0f);
		myRigidbody = GetComponent<Rigidbody> ();
		myRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
	}



	public void ReportScore(){

		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			// Note: make sure to add 'using GooglePlayGames'
			/*Social.ReportScore(ScoreManager.GetScore(),
				GPGSIds.leaderboard_leaderboard,
				(bool success) =>
				{
					//	log.text = "Leaderboard : " + success;
				});*/
		}

	}

	IEnumerator Acceleration(float speed, float speedToReach){
		float speedAnimation = 0.3f;

		hasAccelerated = true;
		while (myRigidbody.velocity.z < speedToReach) {
			speed += speedAnimation;
			myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, speed);

			yield return new WaitForFixedUpdate ();
		}
		hasAccelerated = false;
		currentSpeed = speedToReach;
	}

	void Update () {

		if (!hasAccelerated) {
			myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, currentSpeed);

		}

		if (!isMoving && !isJump && !rotateCtrl.isActiveAndEnabled) {
			transform.rotation = new Quaternion(0f, 0f, 1f, 0f);
			if (transform.localPosition.y < 0f) {
				Debug.Log ("Buggggg");
				transform.localPosition = new Vector3 (transform.localPosition.x, 1, transform.localPosition.z);

			}
		}
		//myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, currentSpeed);

		bool cubeCanMove = gameObject.GetComponent<Rigidbody> ().constraints == RigidbodyConstraints.None;

		if (!LifeManager.GetIsDead () && cubeCanMove) {
			if(!isMoving)
				transform.position = new Vector3 (positionPlayer, transform.position.y, transform.position.z);

			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					if (touch.position.x < Screen.width / 2 && !isMoving && !LifeManager.GetIsDead () && !isJump && positionPlayer > -2) {
						positionPlayer--;
						isMoving = true;
						pivot = transform.position;
						pivot -= moveDirection * 0.5f; 
						pivot -= Vector3.up * 0.5f;
						Vector3 axis = Vector3.forward;
						float degrees = 90;
						StartCoroutine (FlipOverEdge (degrees, pivot, axis));

					} else if (touch.position.x > Screen.width / 2 && !isMoving && !LifeManager.GetIsDead () && !isJump && positionPlayer < 2) {
						positionPlayer++;
						isMoving = true;
						pivot = transform.position;
						pivot += moveDirection * 0.5f; 
						pivot -= Vector3.up * 0.5f;
						Vector3 axis = Vector3.forward;
						float degrees = -90;
						StartCoroutine (FlipOverEdge (degrees, pivot, axis));
					}
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) && !isMoving && !LifeManager.GetIsDead () && !isJump && positionPlayer > -2) {
				positionPlayer--;
				isMoving = true;
				pivot = transform.position;
				pivot -= moveDirection * 0.5f; 
				pivot -= Vector3.up * 0.5f;
				Vector3 axis = Vector3.forward;
				float degrees = 90;
				StartCoroutine (FlipOverEdge (degrees, pivot, axis));
			} else if (Input.GetKeyDown (KeyCode.RightArrow) && !isMoving && !LifeManager.GetIsDead () && !isJump && positionPlayer < 2) {
				positionPlayer++;
				isMoving = true;
				pivot = transform.position;
				pivot += moveDirection * 0.5f; 
				pivot -= Vector3.up * 0.5f;
				Vector3 axis = Vector3.forward;
				float degrees = -90;
				StartCoroutine (FlipOverEdge (degrees, pivot, axis));
			} 

		}
	}


	public void initializePlayerMaterial(){
		ShopManager.Instance.Start ();
		//skin = ShopManager.Instance.getSkin (PlayerSkin.getIndexPlayerSkin());
	//	gameObject.GetComponent<Renderer> ().material = playerMaterial;
		gameObject.GetComponent<Renderer> ().enabled = true;
	}
		
	public void RotateSkin(){
		GameObject skin = transform.GetChild (0).gameObject;
		switch (positionPlayer) {
		case -2: 
			skin.transform.localPosition = new Vector3 (0, 0, 0);
			skin.transform.localEulerAngles = new Vector3 (0, 0, 0);

			break;

		case -1: 
			skin.transform.localPosition = new Vector3 (-0.5f, 0.5f, 0);
			skin.transform.localEulerAngles = new Vector3 (0, 0, -90);

			break;

		case 0: 
			
			skin.transform.localPosition = new Vector3 (0f,1f,0f);
			skin.transform.localEulerAngles = new Vector3 (0, 0, 180);
			break;


		case 1: 
			skin.transform.localPosition = new Vector3 (0.5f, 0.5f, 0);

			skin.transform.localEulerAngles = new Vector3 (0, 0, 90);

			break;

		case 2: 
			skin.transform.localPosition = new Vector3 (0, 0, 0);
			skin.transform.localEulerAngles = new Vector3 (0, 0, 0);
			break;


		}

	

	}
	IEnumerator FlipOverEdge( float degrees, Vector3 pivot, Vector3 axis ) {
	//	Debug.Log (pivot);
	//	GameObject skin = transform.GetChild (0).gameObject;
		int frames = 5;
		float degreesPerFrame = degrees / ((float)frames);
		for (int i = 1; i <= frames; i++) {
			if(!isJump)
				transform.RotateAround (pivot, axis, degreesPerFrame);
			yield return new WaitForFixedUpdate();
		}

		//skin.transform.localEulerAngles = new Vector3 (0, 0, skin.transform.localEulerAngles.z);
		transform.localPosition = new Vector3 (positionPlayer, transform.localPosition.y, transform.localPosition.z);
		RotateSkin ();
		isMoving = false;
		//transform.localEulerAngles = Vector3.zero;
	}

	IEnumerator JumpPlayer( float degrees, Vector3 axis ) {
		//myRigidbody.constraints = RigidbodyConstraints.None;
		SoundManager.Instance.playJumpAudio ();
		//transform.localEulerAngles = Vector3.zero;

		while (myRigidbody.velocity.y > 0) {
			
			yield return new WaitForFixedUpdate ();
		}
		transform.localPosition = new Vector3 (positionPlayer, transform.localPosition.y, transform.localPosition.z);
		//transform.localEulerAngles = Vector3.zero;
		isJump = false;
		jumpCollider.enabled = false;
		basicCollider.enabled = true;
	}
		

	void SetScore(){
		ScoreManager.IncrementScore ();
		currentScoreText.text = ScoreManager.GetScore().ToString ();
	}


	void OnTriggerEnter(Collider other){ 
		switch(other.tag){
		case "ChangePlatform":
			gameController.generatePlatform ();
			gameController.cleanOutPlatforms ();
			if (!LifeManager.GetIsDead ()) {
				
				SetScore ();
			}
			break; 
		case "Jump":
			jumpCollider.enabled = true;
			basicCollider.enabled = false;
			Vector3 axis;
			switch (positionPlayer) {
			case -2: 
				axis = Vector3.left;
				break;
			case -1: 
				axis = Vector3.back;
				break;
			case 0: 
				axis = Vector3.right;
				break;
			case 1: 
				axis = Vector3.forward;
				break;
			case 2: 
				axis = Vector3.left;
				break;
			default: 
				axis = Vector3.left;
				break;
			}
			RotateSkin();
			myRigidbody.velocity = new Vector3 (0f, jumpImpulsion, myRigidbody.velocity.z);
			isJump = true;

			float degrees = 45;
			StartCoroutine(JumpPlayer(degrees , axis));
			break;

		case "Acceleration":
			stateStep++;
			StartCoroutine (Acceleration (currentSpeed, currentSpeed + addAcceleration));
			LaunchAnimation[] launchAnimations = FindObjectsOfType (typeof(LaunchAnimation)) as LaunchAnimation[];
			LaunchAnimationFallCube[] launchAnimationsFallCube = FindObjectsOfType (typeof(LaunchAnimationFallCube)) as LaunchAnimationFallCube[];
			setLaunchAnimation (launchAnimations);
			setLaunchAnimationFallCube (launchAnimationsFallCube);
			other.transform.parent.GetChild (6).gameObject.SetActive (true);
			other.transform.parent.GetChild (7).gameObject.SetActive (true);

			break;


		}
		if((other.tag == "Finish" || other.tag == "GameOver") && !LifeManager.GetIsDead() )
		{
			SoundManager.Instance.playExplosionAudio ();
			PlayerPrefs.SetString ("Date", System.DateTime.Now.ToString());

			isDead = true;
			mainCamera.GetComponent<CameraController> ().Dezoom ();
			StartParticleDestroyPlayer ();
			LifeManager.SetIsDead (true);
			PlayerPrefs.SetInt ("CurrentScore", ScoreManager.GetScore());
			ReportScore ();
			AchievementManager.ReportScore ();
			CreditManager.SetCredits (ScoreManager.GetScore ());
			credit.text = CreditManager.GetCredits ().ToString();
			LifeManager.TakeLife (1);
		}
	
	}
	public int GetPositionToLaunch(){
		int positionToLaunch = 0;
		if (stateStep == 0) {
			positionToLaunch = initialPositionToLaunch;
		} else if (stateStep == 1) {
			positionToLaunch = positionToLaunchAnimationFirstStep;
		} else if (stateStep == 2) {
			positionToLaunch = positionToLaunchAnimationSecondStep;
		}
		return positionToLaunch;
	}

	void setLaunchAnimation(LaunchAnimation[] launchAnimations){

		foreach (LaunchAnimation launch in launchAnimations) {
			launch.positionZ = GetPositionToLaunch ();
		}
	}

	void setLaunchAnimationFallCube(LaunchAnimationFallCube[] launchAnimationsFallCube){

		foreach (LaunchAnimationFallCube launch in launchAnimationsFallCube) {
			launch.positionZ = GetPositionToLaunch () + 10;
		}
	}

	public void showAchievements(){
		SoundManager.Instance.playButtonClickAudio ();
	}

	public void showLeaderboard(){
		SoundManager.Instance.playButtonClickAudio ();
	}

	public void switchSound(){
		bool soundOn = SoundManager.Instance.getState ();
		Debug.Log (soundOn);
		if(soundOn){
			SoundManager.Instance.playButtonClickAudio ();
			SoundManager.Instance.turnOff ();
			setSoundState (false);
		}
		else{
			SoundManager.Instance.turnOn();
			SoundManager.Instance.playButtonClickAudio ();
			setSoundState (true);
		}
	}

	public void setSoundState(bool soundOn){
		Image[] images = soundButton.GetComponentsInChildren<Image> ();
		images [1].enabled = soundOn;
		images [2].enabled = !soundOn;
	}


	IEnumerator startCubeWaitingRotation(){
		while(true){
		gameObject.transform.Rotate(new Vector3(0f, 1f, 0f));
		yield return new WaitForFixedUpdate ();
		}
	}

	public void StartParticleDestroyPlayer(){

		transform.GetChild(0).gameObject.SetActive(false);
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		Vector3 positionPlayer = new Vector3 (gameObject.transform.position.x - 1f, gameObject.transform.position.y, gameObject.transform.position.z);
		ParticleSystem particle = Instantiate (particleDestroyPlayer, positionPlayer,particleDestroyPlayer.transform.rotation);
		particle.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		//particle.GetComponent<Renderer> ().material = transform.GetChild(0).GetComponent<Renderer> ().material;
		particle.Play ();
	}

	public void StartSkinRotation(){
		GameObject skin = transform.GetChild (0).gameObject;
		skin.transform.localPosition = new Vector3 (0f,1f,0f);
		skin.transform.localEulerAngles = new Vector3 (0, 0, 180);

	}

}
