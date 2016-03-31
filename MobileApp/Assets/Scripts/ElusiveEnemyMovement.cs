using UnityEngine;
using System.Collections;

public class ElusiveEnemyMovement : MonoBehaviour {

	public Vector2 startWait;
	public Vector2 movementTime;
	public Vector2 movementWait;
	public float dodge;
	private float targetMovement;
	private Rigidbody rb;
	public float tilt;
	public float speedControl;
	private float currentSpeed;
	public Boundary boundary;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade());
	}

	//we're going to set a value along the x-axis and move towards it over a period of time
	IEnumerator Evade(){

		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));

		//loop for the enemies movement
		while (true) {

			targetMovement = Random.Range (1, dodge ) * -Mathf.Sign(transform.position.x);//give the enemy a random range to move to
			yield return new WaitForSeconds (Random.Range(movementTime.x, movementTime.y));
			targetMovement = 0;//set back to 0 so the enemy doesn't move constantly
			yield return new WaitForSeconds (Random.Range(movementWait.x, movementWait.y));

		}

	}
	
	void FixedUpdate () {
		
		//Mathf.MoveTowards makes sure the speed never exceeds maxDelta.
		//Negative  values of maxDelta are pushed away from the target
		float newMovement = Mathf.MoveTowards (rb.velocity.x, targetMovement, Time.deltaTime * speedControl);
		rb.velocity = new Vector3 (newMovement, 0.0f, currentSpeed);

		//this will keep the enemy ship inside our boundary
		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x, boundary.xMinimum, boundary.xMaximum),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMinimum, boundary.zMaximum)
		);
		//tilts the enemy ship when moving
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
