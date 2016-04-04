using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] asteroids;
	public int asteroidCount;
	public Vector3 spawnValue;
	public float spawnDelay;
	public float startDelay;
	public float wait;//wait for the waves of asteroids
	public GUIText scoreText;
	private int score;
	public GUIText restartText;
	public GUIText gameOverText;
	private bool gameOver;
	private bool restart;
	public string playerName = "";
	public InputField Field;
	public Button Enter;
	public Button Return;
	private string secretKey = "Gareth";
	public string addScoreURL = "http://lynskey.cloudapp.net/addscore.php?";

	void Start(){
		
		gameOver = false;
		restart = false;
		//these two GUIText labels will display nothing at the start of the game
		gameOverText.text = "";
		restartText.text = "";

		score = 0;
		UpdateScore ();
		StartCoroutine (Spawn ());

		Field.gameObject.SetActive (false);
		Enter.gameObject.SetActive (false);
		Return.gameObject.SetActive (false);
	}

	void Update ()
	{
		if (restart)
		{
			
			//if restart is true and the 'R' key is pressed, the scene is reloaded 
			if (Input.GetKeyDown (KeyCode.KeypadEnter))
			{
				//Application.LoadLevel (Application.loadedLevel);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
		
	IEnumerator Spawn(){

		yield return new WaitForSeconds(startDelay);
		//infinite loop that will continuously spawn asteroids and not just the 10 that's it's set to
		while(true){
			//this will spawn multiple asteroids randomly
			for (int i = 0;i < asteroidCount;i++) {
				GameObject asteroid = asteroids[Random.Range(0,asteroids.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (asteroid, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnDelay);
			}
			yield return new WaitForSeconds(wait);

			//this breaks the infinite while
			if (gameOver) {
				restartText.text = "Press 'Enter' to restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore(){
		//scoreText.text = "Score: " + score;
		scoreText.text = "Score: " + score.ToString().PadLeft(6,'0');
	}

	public void GameOver(){
		gameOverText.text = "Game Over!";
		gameOver = true;
		Field.gameObject.SetActive (true);
		Enter.gameObject.SetActive (true);
		Return.gameObject.SetActive (true);
	}

	public void submitScore(){
		StartCoroutine (PostScores(playerName, score));
		Field.gameObject.SetActive (false);
		Enter.gameObject.SetActive (false);
		Return.gameObject.SetActive (true);
	}

	public void enterBtn(){
		playerName = Field.text;
		submitScore ();
	}

	public void ReturnBtn(){
		Application.LoadLevel ("MainMenu");
	}

	public string Md5Sum(string strToEncrypt){

		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding ();
		byte[] bytes = ue.GetBytes (strToEncrypt);

		//encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash (bytes);

		//Convert the encrypted bytes back to a string(base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString (hashBytes [i], 16).PadLeft (2, '0');
		}
		return hashString.PadLeft (32, '0');
	}

	IEnumerator PostScores(string playerName, int score){

		//This connect to a server side php script that will add the name and score to a MySQL database.
		//Supply it with a string representing the players name and the players score.
		string hash = Md5Sum(playerName + score + secretKey);

		string post_url = addScoreURL + "playerName=" + WWW.EscapeURL (playerName) + "&score=" + score + "&hash=" + hash;

		//post the URl to the site and create a download object to get the results
		WWW hs_post = new WWW(post_url);
		yield return hs_post;//wait until the download is done

		if(hs_post.error != null){

			print ("There was an error posting the highscore: " +hs_post.error);
		}
	}
}