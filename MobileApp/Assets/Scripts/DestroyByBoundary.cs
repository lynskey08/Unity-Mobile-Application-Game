using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit(Collider other){
		//destroys objects that leave the boundary 
		Destroy(other.gameObject);
	}
}