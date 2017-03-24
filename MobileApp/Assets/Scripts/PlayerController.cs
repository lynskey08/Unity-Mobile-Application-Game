using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMinimum, xMaximum, zMinimum, zMaximum;
}

public class PlayerController : MonoBehaviour 
{
	public Rigidbody rb;//Rigidbody of the ship
	public float speed;//the speed of the ship
	public float tiltShip;//how much the player ship tilts
	public Boundary boundary;//boundary for the player

	public GameObject laserShot;//ship laser game object
	public Transform laserShotSpawn;//ship laser spawn location
	public float rateOfFire;//rate at which each laser firse

	private float nextTimeFired;

    private Quaternion _antiYaw = Quaternion.identity;
    private float _referenceRoll = 0.0f;

    public ThalmicMyo thalmicMyo;


    void Start() 
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextTimeFired)
		{
			nextTimeFired = Time.time + rateOfFire;
			Instantiate(laserShot, laserShotSpawn.position, laserShotSpawn.rotation);
			GetComponent<AudioSource>().Play ();//this plays the audio file attached to this script
		}
	}
	
	void FixedUpdate () 
	{
		//moves the player 
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
		rb.position = new Vector3 
		(
			//boundary to keep the ship inside the game view
			//clamps a value between a minimum and maximum float value
			Mathf.Clamp (rb.position.x, boundary.xMinimum, boundary.xMaximum), 
		    0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMinimum, boundary.zMaximum)
        );
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tiltShip);//tilts the player ship
	} 
}﻿