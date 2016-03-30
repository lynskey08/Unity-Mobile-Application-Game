using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
	public int asteroidCount;
	public Vector3 spawnValue;
	public float spawnDelay;
	public float startDelay;
	public float wait;//wait for the waves of asteroids
	
	void Start(){
		StartCoroutine (spawn ());
	}

	IEnumerator spawn(){

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
		}
	}
}
