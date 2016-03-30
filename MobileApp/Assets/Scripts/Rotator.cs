using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float asteroidTumble;
	
	void Start () {

		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * asteroidTumble;

	}
}
