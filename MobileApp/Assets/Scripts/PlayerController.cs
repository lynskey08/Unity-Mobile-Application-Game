using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

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
    public GameObject myo = null;
    private Pose _lastPose = Pose.Unknown;
    private bool shooting = false;
    private int i;
    private bool shotsFired = false;

    void Shooting(bool shotsFired)
    {
        if((shotsFired) && Time.time > nextTimeFired)
        {
            nextTimeFired = Time.time + rateOfFire;
            Instantiate(laserShot);
        }
    }

    void Start() 
	{
		rb = GetComponent<Rigidbody> ();

        //Set
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
    }

	void Update()
	{
        bool updateReference = false;
        

        if (updateReference)
        {
            Vector3 movement = new Vector3(myo.transform.forward.x * 10, 0.0f, 0.0f);
            rb.velocity = movement * speed;

            rb.position = new Vector3
            (Mathf.Clamp(rb.position.x, boundary.xMinimum, boundary.xMaximum),
             0,
             Mathf.Clamp(rb.position.z, boundary.zMinimum, boundary.zMaximum)
            );
            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tiltShip);

        }
        rb.position = new Vector3((myo.transform.forward.x * 10), 0.0f, 0.0f);
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

    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}﻿