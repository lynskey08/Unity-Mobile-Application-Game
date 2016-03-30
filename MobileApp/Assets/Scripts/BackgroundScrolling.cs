using UnityEngine;
using System.Collections;

public class BackgroundScrolling : MonoBehaviour {

	public float scrollingSpeed;
	public float tileSize;

	private Vector3 startingPoint;

	void Start () 
	{
		startingPoint = transform.position;
	}

	void Update ()
	{
		float newPosition = Mathf.Repeat (Time.time * scrollingSpeed, tileSize);//repeats the position of the background
		transform.position = startingPoint + Vector3.forward * newPosition;
	}
}