using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using UnityEngine.UI;

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
    public GameObject laserShot1;//ship laser game object
    public Transform laserShotSpawn;//ship laser spawn location
	public float rateOfFire;//rate at which each laser firse

	private float nextTimeFired;

    private Quaternion _antiYaw = Quaternion.identity;
    private float _referenceRoll = 0.0f;
    
    public ThalmicMyo thalmicMyo;
    public GameObject myo = null;
    private Pose _lastPose = Pose.Unknown;
    private bool shooting = false;
    private bool shotsFired = false;
    public Button pause;
    public Button resume;
    //public GameObject gunfire;
    private int i;

    void Start() 
	{
		rb = GetComponent<Rigidbody> ();
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
        i = 0;
    }

    void Shooting(bool shotsFired)
    {
        //for (var i = 0; i < laserShot.Length; i += 1)
        //{
            if ((shotsFired) && Time.time > nextTimeFired)
            {
                nextTimeFired = Time.time + rateOfFire;
                if (i.Equals(0))
                {
                    Instantiate(laserShot, laserShotSpawn.position, laserShotSpawn.rotation);
                }
                else if(i.Equals(1))
                {
                    Instantiate(laserShot1, laserShotSpawn.position, laserShotSpawn.rotation);
                }

            }
            /*if (Input.GetButton("Fire1") && Time.time > nextTimeFired)
            {
                nextTimeFired = Time.time + rateOfFire;
                //creates a clone of laserShot and spawns it at a certain position and rotation
                Instantiate(laserShot[i], laserShotSpawn.position, laserShotSpawn.rotation);
            }*/
        //}

    }

    void Update()
	{
        bool updateReference = false;
        Shooting(shotsFired);
        //gunfire = laserShot[i];

        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;
            shotsFired = false;
            if (thalmicMyo.pose == Pose.Fist)
            {
                //Debug.Log("Fist" + i);
                shotsFired = true;
                thalmicMyo.Vibrate(VibrationType.Medium);
                nextTimeFired = Time.time + rateOfFire;
                i = 0;
                Instantiate(laserShot, laserShotSpawn.position, laserShotSpawn.rotation);
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                //Debug.Log("Fingers" + i);
                shotsFired = true;
                thalmicMyo.Vibrate(VibrationType.Medium);
                nextTimeFired = Time.time + rateOfFire;
                i = 1;
                Instantiate(laserShot1, laserShotSpawn.position, laserShotSpawn.rotation);
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.WaveOut)
            {
                resume.onClick.Invoke();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.WaveIn)
            {
                pause.onClick.Invoke();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }
        

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