// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// /// <summary>
// /// This component works in concert with <see cref="TouchManager"> to handle multitouch fingers. This component calculates status information like dragging information.
// /// </summary>
// public class FingerObj : MonoBehaviour {
// 	/// <summary>
// 	/// Finger Touches start as a `Tap` and end as `Ending`, however they may become `Dragging` if the finger moves significantly.
// 	/// </summary>
// 	public enum TouchState{ Tap, Dragging, Ending, LAST }
// 	public delegate void StatusChange(FingerObj callback);
// 	/// <summary>
// 	/// Subscribe to get updates when the touchstate changes. Takes `FingerObj` as the only argument. 
// 	/// </summary>
// 	public event StatusChange OnStatusChange;
// 	/// <summary>
// 	/// Fires every update so that order is preserved where necessary. Takes `FingerObj` as the only argument. 
// 	/// </summary>
// 	public event StatusChange OnFingerUpdated;

// 	public TouchState currentState = TouchState.Tap;
// 	public int fingerID;
// 	public Touch touch;
// 	public Touch touchLastFrame;
// 	public Touch originTouch;
// 	bool calledThisFrameYet = false;
// 	// Use this for initialization

// 	public void UpdateFinger() {
// 		if( !calledThisFrameYet ) {
// 			gameObject.name = "Finger - " + fingerID;
// 			calledThisFrameYet = true;
// 			touchLastFrame = touch;
// 			touch = TouchHelper.GetTouchByFingerID( fingerID );
// 			Vector3 newPos = TouchHelper.GetTouchWorldPosition( touch );
// 			newPos.z = 0f;
// 			transform.position = newPos;

// 			// determine the 🗾 state
// 			if( currentState == TouchState.Tap ) {
// 				// are we a drag 👑 yet?
// 				Vector2 distance = touch.position - originTouch.position;
// 				if( distance.magnitude > TouchManager.inst.dragDetectionDistPixels ) {
// 					currentState = TouchState.Dragging;
// 					if( OnStatusChange != null)
// 						OnStatusChange( this );
// 				}
// 			} 

// 			// or are we to 💀 die
// 			if ( IsFingerLeaving(this) ) {
// 				currentState = TouchState.Ending;
// 				if( OnStatusChange != null)
// 					OnStatusChange( this );
// 				Destroy( this.gameObject );
// 			}
// 			if( OnFingerUpdated != null )
// 				OnFingerUpdated( this );
// 		}
// 	}

// 	/// <summary>
// 	/// LateUpdate is called every frame, if the Behaviour is enabled.
// 	/// It is called after all Update functions have been called.
// 	/// </summary>
// 	void LateUpdate() {
// 		calledThisFrameYet = false; // a little bit of safety
// 	}

// 	/// <summary>
// 	/// This function is called when the MonoBehaviour will be destroyed. See https://docs.unity3d.com/Manual/ExecutionOrder.html
// 	/// </summary>
// 	void OnDestroy() {
// 		TouchManager.inst.FingerDestroyed(fingerID);
// 		// Debug.Log(gameObject.name + " is being destroyed.");
// 	}

// 	/// <summary>
// 	/// Callback to draw gizmos that are pickable and always drawn.
// 	/// </summary>
// 	void OnDrawGizmos() {
// 		if( fingerID != 0 )
// 			Gizmos.DrawWireSphere( transform.position, TouchHelper.GetTouchByFingerID(fingerID).radius );
// 	}

// 	public static bool IsFingerLeaving( FingerObj finger ) {
// 		return finger.touch.phase == TouchPhase.Canceled || finger.touch.phase == TouchPhase.Ended;
// 	}
// }
