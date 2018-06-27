using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[Header("Behavior")]
	public bool restrictZAxisMovement = true;
	public float percentDistanceToCoverPerFrame = .5f;
	[Header("Setup")]
	[SerializeField] Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () {
		Vector3 targetCleaned = target.position;
		if( restrictZAxisMovement )
			targetCleaned.z = transform.position.z;
		transform.position = Vector3.Lerp( transform.position, targetCleaned, percentDistanceToCoverPerFrame);
	}

	public void SetTarget( Transform newTarget ) {
		target = newTarget;
	}
}
