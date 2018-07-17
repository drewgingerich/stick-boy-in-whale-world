// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// /// <summary>
// /// Helpers made by Matthew Conto for use in multitouch Unity environments. Make sure to check out my _sick_ XML documentation.
// /// </summary>
// public class TouchHelper {

// 	public static int FAKE_FINGER_ID = 99;
// 	/// <summary>
// 	/// Returns the World Position of the finger touch. Note, if there is no background, the touch will be pointed to the max render distance of the camera, leading to strange results.
// 	/// </summary>
// 	/// <returns>Returns Vector3 in world space. Unity automatically ignores the `.z` portion of the Vector3 to get the Vector2.</returns>
// 	public static Vector3 GetTouchWorldPosition(Touch touch) {
// 		Vector3 processed = Camera.main.ScreenToWorldPoint( touch.position );
// 		return processed;
// 	}

// 	/// <summary>
// 	/// Rotates the original GameObject to face the target world position.
// 	/// </summary>
// 	/// <param name="original">Object to rotate</param>
// 	/// <param name="target">Location to face in worldspace</param>
// 	public static void RotateToFace2D( Transform original, Vector3 target ) {
// 		Vector3 vectorToTarget = original.position - target;
// 		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
// 		original.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
// 	}

// 	/// <summary>
// 	/// Rotates using **RigidBody Physics** the original GameObject to face the target world position.
// 	/// </summary>
// 	/// <param name="original">Rigidbody2D of GameObject to rotate</param>
// 	/// <param name="target">Location to face in worldspace</param>
// 	public static void RotateToFace2D( Rigidbody2D original, Vector3 target ) {
// 		Vector3 vectorToTarget = original.transform.position - target;
// 		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
// 		original.MoveRotation( angle );
// 	}

// 	/// <summary>
// 	/// Returns what objects are underneath the finger.
// 	/// </summary>
// 	/// <param name="tag">Optionally only return objects with specified Unity Tag</param>
// 	/// <param name="expandRadiusBy">Optionally expand the effective search size of the finger by this radius value</param>
// 	/// <returns>List of all objects if `tag` is not specified or only the objects with the `tag` specified</returns>
// 	public static List<GameObject> WhatIsUnderTouch( Touch finger, string tag = null, float expandRadiusBy = 0f) {

// 		Vector2 origin = GetTouchWorldPosition(finger);
// 		float radius = finger.radius;
// 		Vector2 direction = Vector2.down;
// 		// RaycastHit2D hit = Physics2D.CircleCast( origin, radius, direction );
// 		Collider2D[] hits = Physics2D.OverlapCircleAll( origin, radius + expandRadiusBy );
// 		// Debug.Log( hit );
// 		List<GameObject> prunedList = new List<GameObject>();
// 		for( int i = 0; i < hits.Length; i++ ) {
// 			if( tag == null || hits[i].gameObject.tag == tag ) {
// 				prunedList.Add( hits[i].gameObject );
// 			}
// 		}

// 		return prunedList;

// 	}

// 	/// <summary>
// 	/// Is the user tapping on a specific object?
// 	/// </summary>
// 	/// <param name="target">Object to look for</param>
// 	/// <param name="expandRadiusBy">Optionally expand the effective search size of the finger</param>
// 	/// <returns></returns>
// 	public static bool IsObjectUnderTouch(Touch finger, Transform target, float expandRadiusBy = 0f ) {
// 		List<GameObject> possibleTargets = WhatIsUnderTouch( finger, target.tag , expandRadiusBy);
// 		foreach( GameObject thisObject in possibleTargets ) {
// 			if( thisObject == target.gameObject ){
// 				return true;
// 			}
// 		}
// 		return false;
// 	}

// 	/// <summary>
// 	/// Find a specific touch of specified ID
// 	/// 
// 	/// _Story time: So it turns out that the <see cref="Touch" /> object isn't kept updated between frames, so the only way to keep track of a finger moving on the screen in a multitouch environment is to use the `FingerID` which does not have it's own easy enumerator-based search function._
// 	/// </summary>
// 	/// <param name="fingerID">Given by Unity's <see cref="Touch.fingerID" /></param>
// 	/// <returns>Returns the struct that contains the information held in this frame's touch.</returns>
// 	public static Touch GetTouchByFingerID( int fingerID ) {
// 		if( fingerID == FAKE_FINGER_ID ) {
// 			// it's our fake mouse!
// 			return GetFakeMouseTouch();
// 		}
// 		foreach( Touch thisTouch in Input.touches ) {
// 			if( thisTouch.fingerId == fingerID ) {
// 				return thisTouch;
// 			}
// 		}
// 		Debug.LogWarning("Finger of ID " + fingerID + " was not found.");
// 		return new Touch();
// 	}

// 	/// <summary>
// 	/// Creates a fake <see cref="Touch" /> struct for what the mouse is doing. Uses <see cref="Touch.fingerId" /> of <see cref="FAKE_FINGER_ID" /> to differentiate itself.
// 	/// </summary>
// 	/// <param name="button">The index for the mouse button</param>
// 	public static Touch GetFakeMouseTouch(int button = 0) {
// 		Touch theTouch = new Touch();
// 		theTouch.fingerId = FAKE_FINGER_ID;
// 		theTouch.position = Input.mousePosition;
// 		theTouch.radius = .01f;
// 		theTouch.deltaPosition = Vector2.zero;
// 		if( Input.GetMouseButtonDown( button ) ) {
// 			theTouch.phase = TouchPhase.Began;
// 		} else if( Input.GetMouseButtonUp( button ) ) {
// 			theTouch.phase = TouchPhase.Ended;
// 		} else if( Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0 ){
// 			theTouch.phase = TouchPhase.Moved;
// 		} else {
// 			theTouch.phase = TouchPhase.Stationary;
// 			theTouch.deltaPosition = new Vector2( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") );
// 		}
// 		return theTouch;
// 	}
// }
