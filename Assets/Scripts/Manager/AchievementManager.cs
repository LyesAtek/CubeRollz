using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
public class AchievementManager : MonoBehaviour {

	private const int SCORE_NEWBIE = 10;
	private const int SCORE_NOVICE = 20;
	private const int SCORE_ROOKIE = 30;
	private const int SCORE_BEGINNER = 40;
	private const int SCORE_TALENTED = 50;
	private const int SCORE_SKILLED = 60;
	private const int SCORE_INTERMEDIATE = 70;
	private const int SCORE_SKILLFUL = 80;
	private const int SCORE_SEASONED = 90;
	private const int SCORE_PROFICIENT = 100;
	private const int SCORE_EXPERIENCED = 110;
	private const int SCORE_ADVANCED = 120;
	private const int SCORE_SENIOR = 130;
	private const int SCORE_EXPERT = 140;



	private int score;


	public static void ReportScore(){
		if (Social.localUser.authenticated) {

			int score = ScoreManager.GetScore ();

		/*	if (score >= SCORE_NEWBIE && !PlayerPrefs.HasKey (GPGSIds.achievement_newbie)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_newbie, 1);
				UnlockAchievement (GPGSIds.achievement_newbie);
			}
			if (score >= SCORE_NOVICE && !PlayerPrefs.HasKey (GPGSIds.achievement_novice)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_novice, 1);
				UnlockAchievement (GPGSIds.achievement_novice);
			}
			if (score >= SCORE_ROOKIE && !PlayerPrefs.HasKey (GPGSIds.achievement_rookie)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_rookie, 1);
				UnlockAchievement (GPGSIds.achievement_rookie);
			}
			if (score >= SCORE_BEGINNER && !PlayerPrefs.HasKey (GPGSIds.achievement_beginner)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_beginner, 1);
				UnlockAchievement (GPGSIds.achievement_beginner);
			}
			if (score >= SCORE_TALENTED && !PlayerPrefs.HasKey (GPGSIds.achievement_talented)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_talented, 1);
				UnlockAchievement (GPGSIds.achievement_talented);
			}
			if (score >= SCORE_SKILLED && !PlayerPrefs.HasKey (GPGSIds.achievement_skilled)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_skilled, 1);
				UnlockAchievement (GPGSIds.achievement_skilled);
			}
			if (score >= SCORE_INTERMEDIATE && !PlayerPrefs.HasKey (GPGSIds.achievement_intermediate)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_intermediate, 1);
				UnlockAchievement (GPGSIds.achievement_intermediate);
			}
			if (score >= SCORE_SKILLFUL && !PlayerPrefs.HasKey (GPGSIds.achievement_skillful)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_skillful, 1);
				UnlockAchievement (GPGSIds.achievement_skillful);
			}
			if (score >= SCORE_SEASONED && !PlayerPrefs.HasKey (GPGSIds.achievement_seasoned)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_seasoned, 1);
				UnlockAchievement (GPGSIds.achievement_seasoned);
			}
			if (score >= SCORE_PROFICIENT && !PlayerPrefs.HasKey (GPGSIds.achievement_proficient)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_proficient, 1);
				UnlockAchievement (GPGSIds.achievement_proficient);
			}
			if (score >= SCORE_EXPERIENCED && !PlayerPrefs.HasKey (GPGSIds.achievement_experienced)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_experienced, 1);
				UnlockAchievement (GPGSIds.achievement_experienced);
			}
			if (score >= SCORE_ADVANCED && !PlayerPrefs.HasKey (GPGSIds.achievement_advanced)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_advanced, 1);
				UnlockAchievement (GPGSIds.achievement_advanced);
			}
			if (score >= SCORE_SENIOR && !PlayerPrefs.HasKey (GPGSIds.achievement_senior)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_senior, 1);
				UnlockAchievement (GPGSIds.achievement_senior);
			}

			if (score >= SCORE_EXPERT && !PlayerPrefs.HasKey (GPGSIds.achievement_expert)) {
				PlayerPrefs.SetInt (GPGSIds.achievement_expert, 1);
				UnlockAchievement (GPGSIds.achievement_expert);
			}*/

		} else {
			//GameObject.Find("EG_Log").GetComponent<Text>().text = "Need to connect";
		}
	}
		
	private static void UnlockAchievement(string achievement){
		Social.ReportProgress(achievement,100.0f, (bool success) => {
			if(success){
				//GameObject.Find("EG_Log").GetComponent<Text>().text = achievement + " unlocked !";
			}
			else{
				//GameObject.Find("EG_Log").GetComponent<Text>().text = "Failed to unlock " + achievement;
			}
		});
	}
}

