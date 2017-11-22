using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform LookTarget;
	private float distance = 25f;

	void Update () {
		transform.position = new Vector3(LookTarget.position.x, LookTarget.position.y + distance, LookTarget.position.z);
	}
}
