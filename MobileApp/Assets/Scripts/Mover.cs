using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;

	void Start()
	{
		//rb = GetComponent<Rigidbody> ();
		//this is for moving the laser
		GetComponent<Rigidbody>().velocity = transform.forward * speed;

	}

}
