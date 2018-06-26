using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GolfMovement : MonoBehaviour {

	enum InputMode { Idle, ReceivingInput, LAST };
	
	[Header("Balance")]
	public float movementForceModifier = .1f;
	
	[Header("Current State")]
	[SerializeField] InputMode currentMode;
	LineRenderer lineRenderer;
	Rigidbody2D rbod;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		rbod = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if( currentMode == InputMode.Idle && Input.touchCount > 0 ) {
			StartCoroutine( GolfSwingRoutine() );
		}
	}

	IEnumerator GolfSwingRoutine() {
		Debug.Log("Touch Down");
		lineRenderer.enabled = true;
		currentMode = InputMode.ReceivingInput;
		lineRenderer.SetPosition(0, transform.position);
		Vector3 touchPos = Vector3.zero;
		while( Input.touches[0].phase != TouchPhase.Ended && Input.touches[0].phase != TouchPhase.Canceled ) {
			Debug.Log(Input.touches[0].phase);
			touchPos = TouchHelper.GetFingerWorldPosition(Input.touches[0]);
			touchPos.z = transform.position.z;
			TouchHelper.RotateToFace2D(rbod, touchPos );
			lineRenderer.SetPosition(1, touchPos );
			yield return null;
		}
		yield return null;
		if( touchPos != Vector3.zero ) {
			//shoot boi
			// because we're already facing `+X` we can just shoot ourselves in that direction
			Vector2 force = (Vector2) touchPos - (Vector2) transform.position; 
			rbod.AddForce( force * movementForceModifier, ForceMode2D.Impulse );
		}
		
		Debug.Log("Touch Up");
		currentMode = InputMode.Idle;
		lineRenderer.enabled = false;
	}
}
