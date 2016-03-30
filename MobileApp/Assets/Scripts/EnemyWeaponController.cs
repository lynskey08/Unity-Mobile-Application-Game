using UnityEngine;
using System.Collections;

public class EnemyWeaponController : MonoBehaviour {

	public GameObject laser;
	public Transform laserSpawn;
	public float rateOfFire;
	public float delay;

	private AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, rateOfFire);
	}

	void Fire ()
	{
		Instantiate (laser, laserSpawn.position, laserSpawn.rotation);
		audioSource.Play ();
	}
}
