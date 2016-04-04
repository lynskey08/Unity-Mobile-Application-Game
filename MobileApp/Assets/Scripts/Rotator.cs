using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float asteroidTumble;
	
	void Start () {
		//rotating of the asteroid
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * asteroidTumble;

	}
}
