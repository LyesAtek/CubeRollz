using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	private GameObject player;
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
	public GameObject plat36;
	public GameObject plat37;
	public GameObject plat38;
	public GameObject plat39;
	public GameObject plat40;
	public GameObject plat41;
	public GameObject plat42;
	public GameObject plat43;
	public GameObject plat44;
	public GameObject plat45;
	public GameObject plat46;
	public GameObject plat47;
	public GameObject plat48;
	public GameObject platAcceleration;
	private int currentPaternIndex = 0;
	private int currentPlatformIndex = 0;
	private GameObject[] platforms;
	private List<List<int>> paternsArray;
	private List<List<int>> antiPaternsArray;
	private List<GameObject> generatedPlatforms = new List<GameObject>();
	private Skin skin;
	public ShopManager shopManager;

 

	public GameObject[] backTab;
 	private int index;

	private int numberOfPlatformGenerated; 
	public int firstStep;
	public int secondStep;
	public int initialPositionToLaunch;
	public int positionToLaunchAnimationFirstStep;
	public int positionToLaunchAnimationSecondStep;

	public void initializePlayerMaterial(){
		shopManager.Start ();
		skin = shopManager.getSkin (PlayerSkin.getIndexPlayerSkin());
		Destroy (player.transform.GetChild (0).gameObject);
		Instantiate (skin.GetSkin (), player.transform);

	}

	public void Start () {
		player = GameObject.Find ("Player");

		StartGame ();
	}

	public void StartGame(){
		numberOfPlatformGenerated = 0 ;

		player.GetComponent<PlayerController> ().StartGame ();

		cleanWorld ();
		initializePlatforms ();
		initializePaterns ();
		initializeAntiPaternsArray ();
		initializePlayerMaterial ();
		generateFirstPlatform ();
		for (int i = 0; i < generatedPlatformsNumber; i++) {
			generatePlatform ();
		}
		numberOfPlatformGenerated -= 1;
		stopAllPlatforms ();

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
			,plat29,plat30,plat31,plat32,plat33,plat34,plat35,plat36,plat37,plat38,plat39,plat40,plat41,plat42,plat43,plat44
			,plat45,plat46,plat47,plat48};
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
		} else {
			GameObject platform = platforms [index];
			GameObject newPlatform = Instantiate (platform, vector, rotation) as GameObject;
			generatedPlatforms.Add (newPlatform);
		}


	}

	bool hasPaternAccepted(int lastPlatform, int platformGenerated){
		for (int i = 0; i < antiPaternsArray.Count; i++) {
			if (antiPaternsArray [i] [0] == lastPlatform && antiPaternsArray [i] [1] == platformGenerated) {
			//	Debug.Log ("Not Accepted");
				return false;
			}
		}
		//Debug.Log ("Accepted");
		return true;
	}



	public void stopAllPlatforms(){
		player.GetComponent<Rotate> ().enabled = true;

		for (var i = 0; i < generatedPlatforms.Count; i++) {
			generatedPlatforms [i].GetComponent<PlatformController> ().moving = false;
		}
		player.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
	}

	IEnumerator stopJump(){
		yield return new WaitForSeconds (0.5f);

		player.GetComponent<Animator> ().enabled = false;
		player.GetComponent<PlayerController> ().StartSkinRotation();
	}
	public void startAllPlatforms(){
		player.GetComponent<Rotate> ().enabled = false;
		player.GetComponent<Animator> ().enabled = true;
		player.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		player.GetComponent<Rigidbody> ().isKinematic = false;
		StartCoroutine(stopJump ());
	}

	public void cleanOutPlatforms(){
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
		antiPaternsArray.Add(new List<int>(){43,7});
		antiPaternsArray.Add(new List<int>(){46,8});
		antiPaternsArray.Add(new List<int>(){40,17});
		antiPaternsArray.Add(new List<int>(){40,16});
		antiPaternsArray.Add(new List<int>(){25,28});
		antiPaternsArray.Add(new List<int>(){16,8});

	}

	public void generatePlatform(){
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

	IEnumerator startCubeWaitingRotation(){
		while(true){
			gameObject.transform.Rotate(new Vector3(0f, 1f, 0f));
			yield return new WaitForFixedUpdate ();
		}
	}



}
