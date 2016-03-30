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
		//find out which if statement works better: do research
		//if (other.tag == ("Boundary") || other.tag == ("Enemy"))

		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
		{
			return;

		}

		if (explosion != null){
			Instantiate (explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		} 
		else {
			gameController.AddScore (scoreValue);
		}

		Destroy(other.gameObject);//destroys the laser
		Destroy(gameObject);//destroys the game object the script is attached to(asteroid)
	}
}
