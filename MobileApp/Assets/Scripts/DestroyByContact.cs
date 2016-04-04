using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if(gameController == null){
			Debug.Log("Can't find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		//find out which if statement works better: look them up
		//if (other.tag == ("Boundary") || other.tag == ("Enemy"))
		//CompareTag seem to be less buggy
		//if there is a collision with the boundary or the enemy
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
		{
			return;

		}
		//if there is an explosion, this will call the explosion prefab
		if (explosion != null)
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}

		//if the player is hit by an object, the player explosion and game over is called
		if (other.CompareTag ("Player"))
		{
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

		gameController.AddScore (scoreValue);//score is added to scoreValue
		Destroy(other.gameObject);//destroys the laser
		Destroy(gameObject);//destroys the game object the script is attached to(asteroid)
	}
}
