using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component works in concert with <see cref="TouchManager"> to handle multitouch fingers. This component calculates status information like dragging information.
/// </summary>
public class FingerObj : MonoBehaviour {
	/// <summary>
	/// Finger Touches start as a `Tap` and end as `Ending`, however they may become `Dragging` if the finger moves significantly.
	/// </summary>
	public enum TouchState{ Tap, Dragging, Ending, LAST }
	public TouchState currentState = TouchState.Tap;
	public int fingerID;
	public Touch touch;
	public Touch touchLastFrame;
	public Touch originTouch;
	bool calledThisFrameYet = false;
	// Use this for initialization

	public void UpdateFinger() {
		if( !calledThisFrameYet ) {
			calledThisFrameYet = true;
			touchLastFrame = touch;
			touch = TouchHelper.GetTouchByFingerID( fingerID );
			transform.position = TouchHelper.GetTouchWorldPosition( touch );

			// determine the 🗾 state
			if( currentState == TouchState.Tap ) {
				// are we a drag 👑 yet?
				Vector2 distance = touch.position - originTouch.position;
				if( distance.magnitude > TouchManager.inst.dragDetectionDistPixels ) {
					currentState = TouchState.Dragging;
				}
			} 

			// or are we to 💀 die
			if ( IsFingerLeaving(this) ) {
				currentState = TouchState.Ending;
				Destroy( this.gameObject );
			}
			
		}
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
		calledThisFrameYet = false; // a little bit of safety
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed. See https://docs.unity3d.com/Manual/ExecutionOrder.html
	/// </summary>
	void OnDestroy() {
		TouchManager.inst.FingerDestroyed(fingerID);
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos() {
		if( fingerID != 0 )
			Gizmos.DrawSphere( transform.position, TouchHelper.GetTouchByFingerID(fingerID).radius );
	}

	public static bool IsFingerLeaving( FingerObj finger ) {
		return finger.touch.phase == TouchPhase.Canceled || finger.touch.phase == TouchPhase.Ended;
	}
}
