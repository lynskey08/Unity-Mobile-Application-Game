using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float lifetime;//this specifies a wait time for destroy to take effect

	void Start(){
		Destroy (gameObject, lifetime);
	}
}
