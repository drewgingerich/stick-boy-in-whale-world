// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraFollow : MonoBehaviour {

// 	[Header("Behavior")]
// 	public bool restrictZAxisMovement = true;
// 	public float percentDistanceToCoverPerFrame = .5f;
// 	[Header("Current State")]
// 	[SerializeField] Transform target;
// 	[SerializeField] float targetCameraZoom;
// 	float originalCameraZoom;
	
// 	Camera cam;

// 	// Use this for initialization
// 	void Start () {
// 		cam = GetComponentInChildren<Camera>();
// 		originalCameraZoom = targetCameraZoom = cam.orthographicSize;
// 	}
	
	
// 	// Update is called once per frame
// 	void Update () {
// 		Vector3 targetCleaned = target.position;
// 		if( restrictZAxisMovement )
// 			targetCleaned.z = transform.position.z;
// 		transform.position = Vector3.Lerp( transform.position, targetCleaned, percentDistanceToCoverPerFrame);
// 		cam.orthographicSize = Mathf.Lerp( cam.orthographicSize, targetCameraZoom, percentDistanceToCoverPerFrame );
// 	}

// 	public void SetTarget( Transform newTarget, float cameraZoom ) {
// 		target = newTarget;
// 		targetCameraZoom = cameraZoom;
// 	}

// 	public void ResetZoom() {
// 		targetCameraZoom = originalCameraZoom;
// 	}

// 	public void ForceRecenter() {
// 		Vector3 targetCleaned = target.position;
// 		targetCleaned.z = transform.position.z;
// 		transform.position = targetCleaned;
// 		cam.orthographicSize = targetCameraZoom;
// 	}
// }
