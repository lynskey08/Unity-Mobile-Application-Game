using UnityEngine;
using System.Collections;

public class BackgroundScrolling : MonoBehaviour {

	public float scrollingSpeed;
	public float tileSize;

	private Vector3 startingPoint;

	void Start () 
	{
		//starting point of the background image
		startingPoint = transform.position;
	}

	void Update ()
	{
		//repeats the position of the background to make it seem like the ship if flying through space
		float newPosition = Mathf.Repeat (Time.time * scrollingSpeed, tileSize);
		transform.position = startingPoint + Vector3.forward * newPosition;
	}
}