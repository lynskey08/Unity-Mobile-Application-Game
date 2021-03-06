﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Canvas MainCanvas;
	public Canvas SettingsCanvas;
	public Canvas HighscoresCanvas;
	private AudioSource audioSource;

	void Awake(){
		SettingsCanvas.enabled = false;
		HighscoresCanvas.enabled = false;
	}

	public void SettingsOn(){
		
		MainCanvas.enabled = false;
		SettingsCanvas.enabled = true;
		HighscoresCanvas.enabled = false;
		audioSource.Play ();
	}

	public void HighscoresOn(){
		
		MainCanvas.enabled = false;
		SettingsCanvas.enabled = false;
		HighscoresCanvas.enabled = true;
		audioSource.Play ();
	}

	public void ReturnOn(){

		MainCanvas.enabled = true;
		SettingsCanvas.enabled = false;
		HighscoresCanvas.enabled = false;
		audioSource.Play ();
	}

	public void LoadOn(){
        //you need to make a return button on the settings and highscores pages
        SceneManager.LoadScene("Main");
	}
}
