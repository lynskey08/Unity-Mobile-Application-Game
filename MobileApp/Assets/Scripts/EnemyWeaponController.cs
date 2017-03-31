using UnityEngine;
using System.Collections;

public class EnemyWeaponController : MonoBehaviour {

	public GameObject laser;
	public Transform laserSpawn;
	public float rateOfFire;
	public float delay;
    private AudioSource audioSource;
    
    public GameController gc;
    public GameObject g;

	void Start ()
    {
        gc = FindObjectOfType<GameController>();

        //Debug.Log(gc.newScoreValue);
        if (gc.newScoreValue < 200)
        {
            audioSource = GetComponent<AudioSource>();
            InvokeRepeating("", delay, rateOfFire);
        }
        if (gc.newScoreValue > 200)
        {
            audioSource = GetComponent<AudioSource>();
            InvokeRepeating("Fire", 0.25f, 500);
        }
        if (gc.newScoreValue > 400)
        {
            audioSource = GetComponent<AudioSource>();
            InvokeRepeating("Fire", 0.00f, 1000);
        }

    }

    void Update()
    {
        gc = FindObjectOfType<GameController>();

        if (gc.newScoreValue > 600)
        {
            audioSource = GetComponent<AudioSource>();
            InvokeRepeating("Fire", 0.00f, 500);
        }
    }

    void Fire ()
	{
		Instantiate (laser, laserSpawn.position, laserSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }
}