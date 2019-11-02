using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Heyzap;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class GameOverController : MonoBehaviour {
	public GameObject player;
	public Text scoreLbl;
	public Text bestScore;
	public Text lastScoreLbl;
	public GameObject menu;
	private ButtonAnimationClick bac;
	public string scene;
	public GameObject popUp;


	private System.DateTime dateConcours;
	private bool hasView = false;
	// Use this for initialization
	public void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		PlayGamesPlatform.Instance.Authenticate (SignInCallback, true);
		ShowContestPopup ();

		HeyzapAds.Start("568e8a5ccdc69ecbe0fe4b9e4ee7ee8d", HeyzapAds.FLAG_NO_OPTIONS);
		HZVideoAd.Fetch();
		if(!PlayerPrefs.HasKey ("PlayerDeath")){
			PlayerPrefs.SetInt ("PlayerDeath", 0);
		}

		HZBannerShowOptions showOptions = new HZBannerShowOptions();
		showOptions.Position = HZBannerShowOptions.POSITION_BOTTOM;
		HZBannerAd.ShowWithOptions(showOptions);

		player = GameObject.Find ("Player");
		showMenu ();

	} 
		
	public void OnContestClic(){
		hasView = false;
		ShowContestPopup ();
	}

	void ShowContestPopup(){
		dateConcours = System.Convert.ToDateTime ("2018/04/01");

		if (System.DateTime.Compare (dateConcours, System.DateTime.Today) >= 0 && !hasView) {
			popUp.SetActive (true);

			hasView = true;
		} else {
			popUp.SetActive (false);

		}
	}

	public void onLeaderboardClick(){
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI ();
		} else {
			SignIn ();
		}
	}

	public void onAchievementClick(){
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowAchievementsUI ();
		} else {
			SignIn ();
		}
	}



	public void SignInCallback(bool success){
		//log.text = "Authenticate : " + success;
	}

	public void SignIn(){

		PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
	}

	public void exit(){
		popUp.SetActive (false);

	}

	public void validate(){
		Application.OpenURL("https://www.electrogames-studio.com/concours-cube-rollz.html");
		popUp.SetActive (false);

	}

	public void ClickPlay(){
		//StartCoroutine(ButtonAnimationClick.clickButton(bestScore,40,5));
		hideMenu();
		ScoreManager.SetScore (0);
		scoreLbl.text = ScoreManager.GetScore ().ToString ();
		GameObject.Find("EG_GameController").GetComponent<GameController>().startAllPlatforms();
	}

	public void ClickShop(){
			//StartCoroutine(ButtonAnimationClick.clickButton(bestScore,40,5));
			/*if (GameObjectMoveManager.isFinish ()) {
				StartCoroutine (GameObjectMoveManager.moveOut (bestScore, "top", 20));
				StartCoroutine (GameObjectMoveManager.moveOut (score, "right", 20));
				StartCoroutine (GameObjectMoveManager.moveOut (credit, "left", 20));
				StartCoroutine (GameObjectMoveManager.moveOut (play, "bottom", 20));
				StartCoroutine (GameObjectMoveManager.moveOut (shop, "bottom", 20));
				StartCoroutine(GameObjectMoveManager.moveOut (life, "top", 20));
				StartCoroutine(GameObjectMoveManager.moveOut (lifeCount, "top", 20));
				StartCoroutine(GameObjectMoveManager.moveOut (gameOver, "left", 20));
				StartCoroutine(GameObjectMoveManager.moveOut (gameOverCount, "right", 20));
				StartCoroutine (Wait ("Shop"));
			}*/
		SceneManager.LoadScene ("Shop");
		
	}

	void showMenu(){
		PlayerPrefs.SetInt ("PlayerDeath", PlayerPrefs.GetInt ("PlayerDeath") + 1);
		if (PlayerPrefs.GetInt ("PlayerDeath") >= 10 && HZInterstitialAd.IsAvailable ()) {
			PlayerPrefs.SetInt ("PlayerDeath", 0);
			HZInterstitialAd.Show ();
		} 
		scoreLbl.gameObject.SetActive (false);
		player.GetComponent<Animator> ().enabled = false;

		int score = ScoreManager.GetScore ();
		ScoreManager.SetBestScore (score);
		/*StartCoroutine(GameObjectMoveManager.moveIn (bestScore, "top", 25));
		StartCoroutine(GameObjectMoveManager.moveIn (score, "top", 32));
		StartCoroutine(GameObjectMoveManager.moveIn (credit, "top", 25));
		StartCoroutine(GameObjectMoveManager.moveIn (play, "bottom", 32));
		StartCoroutine(GameObjectMoveManager.moveIn (shop, "bottom", 25));
		StartCoroutine(GameObjectMoveManager.moveIn (life, "top", 30));
		StartCoroutine(GameObjectMoveManager.moveIn (lifeCount, "top", 32));
		StartCoroutine(GameObjectMoveManager.moveIn (gameOver, "left", 25));
		StartCoroutine(GameObjectMoveManager.moveIn (gameOverCount, "right", 25));*/
		menu.SetActive (true);
		bestScore.text = "Hight Score : " + ScoreManager.GetBestScore ().ToString ();
		lastScoreLbl.text = "LastScore : " +score.ToString ();

	}

	void hideMenu(){
		scoreLbl.gameObject.SetActive (true);
		/*if (GameObjectMoveManager.isFinish ()) {
			StartCoroutine (GameObjectMoveManager.moveOut (bestScore, "top", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (score, "right", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (credit, "left", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (play, "bottom", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (shop, "bottom", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (life, "top", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (lifeCount, "top", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (gameOver, "left", 30));
			StartCoroutine (GameObjectMoveManager.moveOut (gameOverCount, "right", 30));
			//StartCoroutine (Wait ("Scene"));
			GameObject.Find("Player").GetComponent<PlayerController>().startAllPlatforms();
		}*/

		menu.SetActive (false);
	}

	IEnumerator Wait(string scene){
		while (!GameObjectMoveManager.isFinish ()) {
			yield return new WaitForFixedUpdate ();
		}
		SceneManager.LoadScene (scene);
	}

	IEnumerator StartGame(){
		while (!GameObjectMoveManager.isFinish ()) {
			yield return new WaitForFixedUpdate ();
		}
	}


	// Update is called once per frame
	void Update () {

	}
}
