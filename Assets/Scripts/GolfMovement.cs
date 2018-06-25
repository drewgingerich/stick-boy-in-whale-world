using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfMovement : MonoBehaviour {

	enum InputMode { Idle, ReceivingInput, LAST };
	
	Vector3 dbg_target;
	
	[Header("Current State")]
	[SerializeField] InputMode currentMode;
	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if( currentMode == InputMode.Idle && Input.touchCount > 0 ) {
			StartCoroutine( GolfSwingRoutine() );
		}
	}

	/// <summary>
	/// Routine for dragging a golf swing
	/// </summary>
	/// <returns></returns>
	IEnumerator GolfSwingRoutine() {
		Debug.Log("Touch Down");
		Touch theTouch = Input.touches[0];
		while( theTouch.phase != TouchPhase.Ended ) {
			
		}
		yield return null;
	}
}
