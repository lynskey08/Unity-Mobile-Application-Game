using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
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

	void Start(){
		
		gameOver = false;
		restart = false;
		//these two GUIText labels will display nothing at the start of the game
		gameOverText.text = "";
		restartText.text = "";

		score = 0;
		UpdateScore ();
		StartCoroutine (Spawn ());
	}

	void Update ()
	{
		if (restart)
		{
			//if restart is true and the 'R' key is pressed, the scene is reloaded 
			if (Input.GetKeyDown (KeyCode.R))
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
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (asteroid, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnDelay);
			}
			yield return new WaitForSeconds(wait);

			//this breaks the infinite while
			if (gameOver) {
				restartText.text = "Press 'R' to restart";
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
	}
}