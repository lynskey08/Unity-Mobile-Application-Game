using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingsPage : MonoBehaviour {

	//private float slider = 1.0f;
	private float volume = 1.0f;
	public Slider volumeSlider;
	private string highscoresURL = "http://lynskey.cloudapp.net/display.php";
	public GameObject scoreList;

	void Start () {
		//loads the currently saved volume on each scene
		volumeSlider.value = PlayerPrefs.GetFloat ("Volume Slider", volumeSlider.value);
		StartCoroutine(HighScoreMenu());
	}

	public void VolumeSlider () {

		volume = volumeSlider.value;
		PlayerPrefs.SetFloat("Volume Slider", volumeSlider.value);
		AudioListener.volume = volume;
		PlayerPrefs.SetFloat("Audio Volume", volume);
	}

	public void Mute()
	{
		volume = 0;
		volumeSlider.value = 0;
		PlayerPrefs.SetFloat("Volume Slider", volumeSlider.value);
		AudioListener.volume = volume;
		PlayerPrefs.SetFloat("Audio Volume", volume);
	}

	IEnumerator HighScoreMenu()
	{
		scoreList.GetComponent<Text>().enabled = true;//Enables Score display
		scoreList.GetComponent<Text>().text = "Loading Scores";
		WWW hs_get = new WWW(highscoresURL);
		yield return hs_get;

		if (hs_get.error != null)
		{
			print("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			scoreList.GetComponent<Text>().text = hs_get.text; // this is a GUIText that will display the scores in game.
		}
	}	
}
