using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPage : MonoBehaviour {

	private float slider = 1.0f;
	private float volume = 1.0f;
	public Slider volumeSlider;

	void Start () {
		//loads the currently saved volume on each scene
		volumeSlider.value = PlayerPrefs.GetFloat ("Volume Slider", volumeSlider.value);
	}

	public void Options () {

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
}
