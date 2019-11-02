using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private AudioSource buttonAudioSource;
	private AudioSource gameAudioSource;
	private AudioSource explosionAudioSource;
	private AudioSource coinAudioSource;
	private AudioSource cashAudioSource;
	private AudioSource jumpAudioSource;
	private bool soundOn;
	public static SoundManager Instance = null;



	void Start () {
		soundOn = true;
		Instance = this;
		initializeButtonClickAudio ();
		initializeGameAudio ();
		initializeExplosionAudio ();
		initializeCoinAudio ();
		initializeCashAudio ();
		initializeJumpAudio ();
	}

	void Update () {
		
	}
		
	//////////////////////////////////////////////////////////////

	public void initializeButtonClickAudio(){
		AudioClip clip = Resources.Load ("Audio/button", typeof(AudioClip)) as AudioClip;
		buttonAudioSource = gameObject.AddComponent<AudioSource> ();
		buttonAudioSource.clip = clip;
		buttonAudioSource.volume = getState() ? 0.2f : 0f;
		buttonAudioSource.loop = false;
	}

	public void initializeGameAudio(){
		AudioClip clip = Resources.Load ("Audio/game", typeof(AudioClip)) as AudioClip;
		gameAudioSource = gameObject.AddComponent<AudioSource> ();
		gameAudioSource.clip = clip;
		gameAudioSource.volume = getState() ? 0.1f : 0f;
		gameAudioSource.loop = true;
	}

	public void initializeExplosionAudio(){
		AudioClip clip = Resources.Load ("Audio/explosion", typeof(AudioClip)) as AudioClip;
		explosionAudioSource = gameObject.AddComponent<AudioSource> ();
		explosionAudioSource.clip = clip;
		explosionAudioSource.volume = getState() ? 0.5f : 0f;
		explosionAudioSource.loop = false;
	}

	public void initializeCoinAudio(){
		AudioClip clip = Resources.Load ("Audio/coin", typeof(AudioClip)) as AudioClip;
		coinAudioSource = gameObject.AddComponent<AudioSource> ();
		coinAudioSource.clip = clip;
		coinAudioSource.volume = getState() ? 0.1f : 0f;
		coinAudioSource.loop = false;
	}

	public void initializeCashAudio(){
		AudioClip clip = Resources.Load ("Audio/cash", typeof(AudioClip)) as AudioClip;
		cashAudioSource = gameObject.AddComponent<AudioSource> ();
		cashAudioSource.clip = clip;
		cashAudioSource.volume = getState() ? 0.4f : 0f;
		cashAudioSource.loop = false;
	}


	public void initializeJumpAudio(){
		AudioClip clip = Resources.Load ("Audio/jump", typeof(AudioClip)) as AudioClip;
		jumpAudioSource = gameObject.AddComponent<AudioSource> ();
		jumpAudioSource.clip = clip;
		jumpAudioSource.volume = getState() ? 0.2f : 0f;
		jumpAudioSource.loop = false;
	}

	//////////////////////////////////////////////////////////////


	public void playGameMusic(){
		if (!gameAudioSource) {
			initializeGameAudio ();

				gameAudioSource.Play ();

		}
		if (!gameAudioSource.isPlaying) {
			if (soundOn) {
				gameAudioSource.Play ();
			}
		}
	}

	public void playButtonClickAudio(){
		if (!buttonAudioSource) {
			initializeButtonClickAudio ();
		}
		if (soundOn) {
			buttonAudioSource.Play ();
		}
	}

	public void playExplosionAudio(){
		if (!buttonAudioSource) {
			initializeExplosionAudio ();
		}
		if (soundOn) {
			explosionAudioSource.Play ();
		}
	}

	public void playCoinAudio(){
		if (!coinAudioSource) {
			initializeCoinAudio ();
		}
		if (soundOn) {
			coinAudioSource.Play ();
		}
	}
		
	public void playCashAudio(){
		if (cashAudioSource) {
			initializeCashAudio ();
		}
		if (soundOn) {
			cashAudioSource.Play ();
		}
	}
		
	public void playJumpAudio(){
		if (jumpAudioSource) {
			initializeJumpAudio ();
		}
		if (soundOn) {
			jumpAudioSource.Play ();
		}
	}

	public void stopMainAudio(){
		if (gameAudioSource && gameAudioSource.isPlaying) {
			gameAudioSource.Stop ();
		}
	}

	public void turnOn(){
		soundOn = true;
		gameAudioSource.Play ();
	}

	public void turnOff(){
		gameAudioSource.Pause ();
		soundOn = false;
	}

	public bool getState(){
		if (soundOn) {
			return true;

		}
		return false;
	}
}
