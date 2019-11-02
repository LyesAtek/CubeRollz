using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerTest : MonoBehaviour {
	private Rigidbody myRigidbody;
	public float moveHorizontal;
	private float time = 0f;
	private float timeToDezoom = 0.2f;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private bool isMoving;
	public float moveVertical;
	public Text currentScoreText;
	public Button soundButton;

	public int jumpFrames;
	public float jumpImpulsion;
	public float offsetZ = 20f;
	public int generatedPlatformsNumber;
	public GameObject plat1;
	public GameObject plat2;
	public GameObject plat3;
	public GameObject plat4;
	public GameObject plat5;
	public GameObject plat6;
	public GameObject plat7;
	public GameObject plat8;
	public GameObject plat9;
	public GameObject plat10;
	public GameObject plat11;
	public GameObject plat12;
	public GameObject plat13;
	public GameObject plat14;
	public GameObject plat15;
	public GameObject plat16;
	public GameObject plat17;
	public GameObject plat18;
	public GameObject plat19;
	public GameObject plat20;
	public GameObject plat21;
	public GameObject plat22;
	public GameObject plat23;
	public GameObject plat24;
	public GameObject plat25;
	public GameObject plat26;
	public GameObject plat27;
	public GameObject plat28;
	public GameObject plat29;
	public GameObject plat30;
	public GameObject plat31;
	public GameObject plat32;
	public GameObject plat33;
	public GameObject plat34;
	public GameObject plat35;
	public GameObject platAcceleration;
	private int currentPaternIndex = 0;
	private int currentPlatformIndex = 0;
	private GameObject[] platforms;
	private List<List<int>> paternsArray;
	private List<List<int>> antiPaternsArray;
	private List<GameObject> generatedPlatforms = new List<GameObject>();

	Vector3 moveDirection = Vector3.right;

	Vector3 pivot;

	private int score;
	private Skin skin;
	public ShopManager shopManager;

	public Material[] materials;
	private int indexMat;
	private int rangeMat ;
	private int countMat; 

	public GameObject[] backTab;
	private GameObject go;
	private int count;
	private Vector3 startBackgroundPosition;
	private Vector3 goPosition;
	private float offsetZBackground = 2f;
	private int index;
	private int rangeBackground ;
	private int positionPlayer;
	private bool isJump;
	private SoundManager soundManager;
	public float speedPlayer;
	private float currentSpeed;
	public float addAcceleration;
	private int numberOfPlatformGenerated; 
	public int firstStep;
	public int secondStep;
	private bool hasAccelerated;
	private int stateStep;
	public int initialPositionToLaunch;
	public int positionToLaunchAnimationFirstStep;
	public int positionToLaunchAnimationSecondStep;
	public void Start () {
		stateStep = 0;
		currentSpeed = speedPlayer;
		numberOfPlatformGenerated = 0;
		GameObject go = GameObject.Find ("SoundManager");
		if (go == null) {
			go = new GameObject ();
			Instantiate (go);
			go.AddComponent<SoundManager> ();
			go.name = "SoundManager";
		}
		soundManager = go.GetComponent<SoundManager> ();
		soundManager.playGameMusic ();
		positionPlayer = 0;
		gameObject.transform.localPosition = new Vector3(0f, 2.36f, -4f);
		gameObject.transform.localRotation = new Quaternion (0f, 0f, 0f, 0f);

		GetComponent<Renderer> ().enabled = true;
		cleanWorld ();
		countMat = 0;
		rangeMat = materials.Length;
		indexMat = Random.Range (0, rangeMat -1);
		startBackgroundPosition = new Vector3 (0.0f, -0.28f, -8.81f);
		rangeBackground = backTab.Length;
		initializePlayerMaterial ();
		score = 0;
		ScoreManager.SetScore (score);

		myRigidbody = GetComponent<Rigidbody> ();
		myRigidbody.isKinematic = false;
		initializePlatforms ();
		initializePaterns ();
		initializeAntiPaternsArray ();
		generateFirstPlatform ();
		for (int i = 0; i < generatedPlatformsNumber; i++) {
			generatePlatform ();
		}
		numberOfPlatformGenerated -= 1;
		stopAllPlatforms ();
		StartCoroutine (setBackgroundColor ());
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
		bool cubeCanMove = gameObject.GetComponent<Rigidbody> ().constraints == RigidbodyConstraints.None;
		if (!isMoving && !isJump) {
			transform.rotation = new Quaternion(0f, 0f, 1f, 0f);
		}
		if (!LifeManager.GetIsDead () && cubeCanMove) {
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
		shopManager.Start ();
		skin = shopManager.getSkin (PlayerSkin.getIndexPlayerSkin());
		//Material playerMaterial = skin.GetMaterial();
		//gameObject.GetComponent<Renderer> ().material = playerMaterial;
		gameObject.GetComponent<Renderer> ().enabled = true;
	}
		

	IEnumerator FlipOverEdge( float degrees, Vector3 pivot, Vector3 axis ) {
		int frames = 5;
		float degreesPerFrame = degrees / ((float)frames);
		for (int i=1; i<=frames; i++) {
			transform.RotateAround( pivot, axis, degreesPerFrame );
			yield return new WaitForFixedUpdate();
		} 
		transform.localPosition = new Vector3 (positionPlayer, transform.localPosition.y, transform.localPosition.z);
		isMoving = false;
	}

	IEnumerator JumpPlayer( float degrees, Vector3 axis ) {
		soundManager.playJumpAudio ();
		transform.rotation = new Quaternion(0f, 0f, 1f, 0f);
		while (myRigidbody.velocity.y > 0) {
			yield return new WaitForFixedUpdate ();
		}
		isJump = false;
	}


	void cleanWorld(){
		for (int i = 0; i < generatedPlatforms.Count; i++) {
			Destroy (generatedPlatforms [i]);
		}
		generatedPlatforms = new List<GameObject>();
	}

	void initializePlatforms(){
		platforms = new GameObject[]{plat1,plat2,plat3,plat4,plat5,plat6,plat7,plat8,plat9,plat10,plat11,plat12,plat13
			,plat14,plat15,plat16,plat17,plat18,plat19,plat20,plat21,plat22,plat23,plat24,plat25,plat26,plat27,plat28
			,plat29,plat30,plat31,plat32,plat33,plat34,plat35};
	}

	void generateFirstPlatform(){
		int index = 0;
		GameObject platform  = platforms[index];
		Quaternion rotation = platform.transform.rotation;
		Vector3 vector = new Vector3(0, 0F, 0F);
		newPlatform(index, vector, rotation);
	}

	void newPlatform(int index, Vector3 vector,Quaternion rotation){
		numberOfPlatformGenerated++;

		if (numberOfPlatformGenerated == firstStep || numberOfPlatformGenerated == secondStep) {
			GameObject platform = platAcceleration;
			GameObject newPlatform = Instantiate (platform, vector, rotation) as GameObject;
			generatedPlatforms.Add (newPlatform);
			GenerateBackground (newPlatform);
		} else {
			GameObject platform = platforms [index];
			GameObject newPlatform = Instantiate (platform, vector, rotation) as GameObject;
			generatedPlatforms.Add (newPlatform);
			GenerateBackground (newPlatform);
		}


	}

	bool hasPaternAccepted(int lastPlatform, int platformGenerated){
		for (int i = 0; i < antiPaternsArray.Count; i++) {
			if (antiPaternsArray [i] [0] == lastPlatform && antiPaternsArray [i] [1] == platformGenerated) {
				Debug.Log ("Not Accepted");
				return false;
			}
		}
		Debug.Log ("Accepted");
		return true;
	}

	void changeIndexMaterial(){
		rangeMat = materials.Length;
		indexMat = Random.Range (0, rangeMat -1);
	}

	public void stopAllPlatforms(){
		GetComponent<Rotate> ().enabled = true;
		currentScoreText.enabled = false;
		for (var i = 0; i < generatedPlatforms.Count; i++) {
			generatedPlatforms [i].GetComponent<PlatformController> ().moving = false;
		}
		gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
	}

	public void startAllPlatforms(){
		GetComponent<Rotate> ().enabled = false;
		GameObject.Find("Player").gameObject.GetComponent<Animator> ().enabled = true;
		currentScoreText.enabled = true;
		/*for (var i = 0; i < generatedPlatforms.Count; i++) {
			generatedPlatforms [i].GetComponent<PlatformController> ().moving = true;
		}*/
		gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		StartCoroutine(stopJump ());
	}

	void cleanOutPlatforms(){
		if(generatedPlatforms.Count > generatedPlatformsNumber + 2){
			Destroy(generatedPlatforms[0]);
			generatedPlatforms.RemoveAt(0);
		}
	}

	bool isLastPlatformSame(int index){
		GameObject lastPlatform = getLastPlatform();
		GameObject platform = platforms[index];
		return platform.tag == lastPlatform.tag;
	}

	void createPlatform(int index){
		GameObject platform  = platforms[index];
		GameObject lastPlatform = getLastPlatform();
		Quaternion rotation = platform.transform.rotation;
		float offset = lastPlatform.transform.GetChild (0).transform.localScale.z;
		float x  = 0;
		float y   = 0;
		float z  = lastPlatform.transform.position.z + offset;
		Vector3 vector = new Vector3(x, y, z);
		newPlatform(index, vector, rotation);
	}


	GameObject getLastPlatform(){
		int lastIndex = generatedPlatforms.Count - 1;
		GameObject lastPlatform  = generatedPlatforms[lastIndex];
		
		return lastPlatform;
	}

	/*
	 * 0 V
	 * 1 CL : Cube Left
	 * 2 CR : Cube Right
	 * 3 CC : Cube Center
	 * 4 FC-2 : Falling Cube Left
	 * 5 FC-1 : Falling Cube Left
	 * 6 FC0 : Falling Cube Center
	 * 7 FC1 : Falling Cube Right
	 * 8 FC2 : Falling Cube Right
	 * 9 SC Spawning Cube
	 * 10 SC2 :  Spawning Cube 
	 * 11 SC3 :  Spawning Cube
	 * 12 J : Jump
	 * 13 MWL - 1 : Mouving Wall Left 
	 * 14 MWL - 2 : Mouving Wall Left 
	 * 15 MWR 2 : Mouving Wall Right 
	 * 16 MWC : Mouving Wall Center
	 * 17 MWR 1 : Mouving WalL Rig
	 * 18 MGL -2 Mouving Gate Left
	 * 19 MGR 2 : Mouving Gate Right
	 * 20 MGR 1 : Mouving Gate Center
	 * 21 MGS : Mouving Gate Sides
	 * 22 MGC : Mouving Gate Center
	 * 23 MGL -1  : Mouving Gate Left
	 * 24 JR 1 : Jump Right 
	 * 25 JR 2 : Jump Right 2
	 * 26 JC : Jump Center
	 * 27 JL -1 : Jump Left
	 * 28 JL -2 : Jump Left
	 * 29 AU : Accordeon UP
	 * 30 AD : Accordeon Down
	 * */
	void initializePaterns(){
		paternsArray = new List<List<int>> ();
		//paternsArray.Add(new List<int>(){13});
		//paternsArray.Add(new List<int>(){25});
		for (int i = 1; i < platforms.Length; i++) {
			paternsArray.Add(new List<int>(){i});
		}
	}


	void initializeAntiPaternsArray(){
		antiPaternsArray = new List<List<int>> ();
		////////  Cube fallings triangle with Jump platforms
		antiPaternsArray.Add(new List<int>(){24,6});
		antiPaternsArray.Add(new List<int>(){25,6});
		antiPaternsArray.Add(new List<int>(){26,6});
		antiPaternsArray.Add(new List<int>(){27,6});
		antiPaternsArray.Add(new List<int>(){28,6});
		antiPaternsArray.Add(new List<int>(){22,6});
		antiPaternsArray.Add(new List<int>(){12,6});



		// Falling Cube Right  with  Mouving Gate/Wall  (-2/-1/1)
		antiPaternsArray.Add(new List<int>(){15,7});
		antiPaternsArray.Add(new List<int>(){18,7});
		antiPaternsArray.Add(new List<int>(){23,7});
		antiPaternsArray.Add(new List<int>(){20,7});
		antiPaternsArray.Add(new List<int>(){21,7});
		antiPaternsArray.Add(new List<int>(){17,7});
		antiPaternsArray.Add(new List<int>(){31,7});

	}
	void generatePlatform(){
		if (currentPlatformIndex >= paternsArray [currentPaternIndex].Count) {
			currentPaternIndex = getPaternIndex ();
			currentPlatformIndex = 0;
		}

		createPlatform (paternsArray [currentPaternIndex] [currentPlatformIndex]);
		currentPlatformIndex++;
	}

	int findIndexPlatform(GameObject plat){
		for (int i = 0; i < platforms.Length; i++) {
			if (plat.name  == platforms [i].name + "(Clone)") {
				return i;
			}
		}
		return 0;
	}

	int getPaternIndex(){
		int index;
		do{
			index = Random.Range (0, paternsArray.Count);
		}while(index == currentPaternIndex  || !hasPaternAccepted(findIndexPlatform(getLastPlatform()),index));
		return index;
	}

	void SetScore(int value){
		ScoreManager.SetScore (value);
		if (ScoreManager.GetScore () % 10 == 0) {
			changeIndexMaterial ();
			//setBackgroundColor ();

			StartCoroutine (setBackgroundColor ());
		}

	}

	/*************Background Controller ***********/// 

	void GenerateBackground(GameObject platform){

		for (int i = 0; i < 10; i++) {
			index = Random.Range (0, rangeBackground);
			go = Instantiate (backTab [index]);
			go.transform.SetParent (platform.transform);
			goPosition = new Vector3 (startPosition.x, startPosition.y, startPosition.z + (offsetZBackground * i));
			go.transform.localPosition = goPosition;
		}
	}


	private IEnumerator setBackgroundColor()
	{
		countMat = 0;
		for (int i = 0; i < generatedPlatforms.Count; i++) {
			foreach (Transform child in generatedPlatforms[i].transform) {
				if (child.name.Contains ("Back")) {
					if (countMat % 2 == 0) {
						child.transform.GetChild (0).GetComponent<Renderer> ().material = materials [indexMat];
						child.transform.GetChild (1).GetComponent<Renderer> ().material = materials [indexMat];
					} else {
						child.transform.GetChild (0).GetComponent<Renderer> ().material = materials [indexMat + 1];
						child.transform.GetChild (1).GetComponent<Renderer> ().material = materials [indexMat + 1];
					}
					countMat++;

				}
			}
			yield return new WaitForSeconds(0.08f);
		}
	}
	void OnTriggerEnter(Collider other){ 
		switch(other.tag){
		case "ChangePlatform":
			generatePlatform ();
			cleanOutPlatforms ();
			if (!LifeManager.GetIsDead ()) {
				score += 1;
				SetScore (score);
			}
			break; 
		case "Jump":
			
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
		soundManager.playButtonClickAudio ();
	}

	public void showLeaderboard(){
		soundManager.playButtonClickAudio ();
	}

	public void switchSound(){
		bool soundOn = soundManager.getState ();
		if(soundOn){
			soundManager.playButtonClickAudio ();
			soundManager.turnOff ();
			setSoundState (false);
		}
		else{
			soundManager.turnOn();
			soundManager.playButtonClickAudio ();
			setSoundState (true);
		}
	}

	public void setSoundState(bool soundOn){
		Image[] images = soundButton.GetComponentsInChildren<Image> ();
		images [1].enabled = soundOn;
		images [2].enabled = !soundOn;
	}

	IEnumerator stopJump(){
		yield return new WaitForSeconds (0.5f);
		GetComponent<Animator> ().enabled = false;
	}

	IEnumerator startCubeWaitingRotation(){
		while(true){
			gameObject.transform.Rotate(new Vector3(0f, 1f, 0f));
			yield return new WaitForFixedUpdate ();
		}
	}


}
