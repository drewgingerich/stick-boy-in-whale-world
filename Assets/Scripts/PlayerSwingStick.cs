using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingStick : MonoBehaviour {
	// public enum StickInteractionType{ Poke, Swing, LAST };
	public enum StickStatus{ Idle, Swinging, LAST };

	[Header("Balance")]
	[SerializeField] float maxTapDistance = 5f;
	[SerializeField] float expandTouchRadiusBy = .5f;

	[Header("Current State")]
	[SerializeField] StickStatus swingState = StickStatus.Idle;
	// public StickInteractionType currentStickInteraction;
	[SerializeField] FingerObj fingerOfInterest;

	[Header("Setup")]
	public GameObject stick;
	// private float stickExpandedSize;
	public float swingTime;
	// [SerializeField] GameObject stickHitParticles;
	// [SerializeField] AnimationCurve debugStickAnimation;

	void OnEnable() {
		swingState = StickStatus.Idle;
		stick.SetActive(false);
		if( TouchManager.inst != null )
			TouchManager.inst.OnNewFinger += OnNewFinger;
	}

	void OnStart() {
		TouchManager.inst.OnNewFinger += OnNewFinger;
	}
	void OnDisable() {
		TouchManager.inst.OnNewFinger -= OnNewFinger;
	}

	void OnNewFinger(FingerObj newFinger) {
		Vector2 distance = (Vector2) newFinger.transform.position - (Vector2)transform.position;
		if( distance.magnitude <= maxTapDistance ) {
			fingerOfInterest = newFinger;
			fingerOfInterest.OnStatusChange += FingerStatusChange;
		}
	}

	void FingerStatusChange( FingerObj foi ) {
		Debug.Log( foi.name + " state's changed to " + foi.currentState );
		if( foi.currentState == FingerObj.TouchState.Ending ) {
			// Swing on finger up
			SwingStick();
		} else if ( foi.currentState == FingerObj.TouchState.Dragging ) {
			// We don't care about fingers being dragged
			fingerOfInterest.OnStatusChange -= FingerStatusChange;
			fingerOfInterest = null;
		}
	}


	

	public void SwingStick() {
		if( swingState == StickStatus.Idle ){
			Debug.Log("Trying to swing stick!");
			RaycastHit2D hit = GetFirstObjHitByStick( fingerOfInterest.touch );
			if( hit ) {
				hit.collider.GetComponent<StickInteractable>().HitByStick( hit );
				Debug.DrawLine(transform.position, fingerOfInterest.transform.position, Color.green, 2f );
			} else {
				Debug.DrawLine(transform.position, fingerOfInterest.transform.position, Color.red, 2f );
			}
			
			StartCoroutine( StickSwingRoutine() );
		}
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos() {
		Gizmos.DrawWireSphere( transform.position, maxTapDistance);
	}

	/// <summary>
	/// Gets the first object in the direction of wherever the user tapped.
	/// </summary>
	/// <returns>Returns the raycast collision itself so that the position of contact can be preserved. Returns an empty `RaycastHit2D` if the object hit does not contain a `<see cref="StickInteractable" />`. You can evaluate a `RaycastHit2D` as a boolean to determine if this function succeeded or failed.</returns>
	RaycastHit2D GetFirstObjHitByStick( Touch finger ) {
		Vector2 tapLocation = TouchHelper.GetTouchWorldPosition(finger);
		float radius = finger.radius + expandTouchRadiusBy;
		Vector2 direction = tapLocation - (Vector2) transform.position;
		RaycastHit2D hit = Physics2D.CircleCast( tapLocation, radius, direction.normalized, Mathf.Max( maxTapDistance, direction.magnitude ) ); 
		// just play the hits 💿 ayy lmao
		if( hit && hit.collider.GetComponent<StickInteractable>() != null ) {
			return hit;
		} else {
			return new RaycastHit2D();
		}
		
	}


	/// <summary>
	/// Just the animation for swinging the stick
	/// </summary>
	IEnumerator StickSwingRoutine() {
		swingState = StickStatus.Swinging;
		stick.SetActive(true);
		for( float timeLeft = swingTime; timeLeft > 0; timeLeft -= Time.deltaTime ) {
			DebugAnimateStick( timeLeft );
			yield return null;

		}
		stick.SetActive(false);
		swingState = StickStatus.Idle;
	}

	void DebugAnimateStick( float timeLeft ) {

	}

	
}
