using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helpers made by Matthew Conto
/// </summary>
public class TouchHelper {
	/// <summary>
	/// Returns the World Position of the finger touch. Note, if there is no background, the touch will be pointed to the max render distance of the camera, leading to strange results.
	/// </summary>
	/// <returns>Vector3 in world space. You can ignore the `.z` portion of the Vector3 to get the Vector2.</returns>
	public static Vector3 GetFingerWorldPosition(Touch touch) {
		Vector3 processed = Camera.main.ScreenToWorldPoint( touch.position );
		return processed;
	}

	/// <summary>
	/// Rotates the original GameObject to face the target worldposition
	/// </summary>
	/// <param name="original">Object to rotate</param>
	/// <param name="target">Location to face in worldspace</param>
	public static void RotateToFace2D( Transform original, Vector3 target ) {
		Vector3 vectorToTarget = original.position - target;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		original.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	/// <summary>
	/// Rotates using **RigidBody Physics** the original GameObject to face the target worldposition
	/// </summary>
	/// <param name="original">Rigidbody2D of GameObject to rotate</param>
	/// <param name="target">Location to face in worldspace</param>
	public static void RotateToFace2D( Rigidbody2D original, Vector3 target ) {
		Vector3 vectorToTarget = original.transform.position - target;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		original.MoveRotation( angle );
	}

	/// <summary>
	/// Sends a ray from the finger to see what gameobjects are underneath.
	/// </summary>
	/// <param name="tag">Optionally only return objects with specified Unity Tag</param>
	/// <param name="expandRadiusBy">Optionally expand the effective search size of the finger</param>
	/// <returns>List of all objects if `tag` is not specified or only the objects with the `tag` specified</returns>
	public static List<GameObject> WhatIsUnderFinger( Touch finger, string tag = null, float expandRadiusBy = 0f) {

		Vector2 origin = GetFingerWorldPosition(finger);
		float radius = finger.radius;
		Vector2 direction = Vector2.down;
		// RaycastHit2D hit = Physics2D.CircleCast( origin, radius, direction );
		Collider2D[] hits = Physics2D.OverlapCircleAll( origin, radius + expandRadiusBy );
		// Debug.Log( hit );
		List<GameObject> prunedList = new List<GameObject>();
		for( int i = 0; i < hits.Length; i++ ) {
			if( tag == null || hits[i].gameObject.tag == tag ) {
				prunedList.Add( hits[i].gameObject );
			}
		}

		return prunedList;

	}

	/// <summary>
	/// Is the user tapping on a specific object?
	/// </summary>
	/// <param name="target">Object to look for</param>
	/// <param name="expandRadiusBy">Optionally expand the effective search size of the finger</param>
	/// <returns></returns>
	public static bool IsObjectUnderFinger(Touch finger, Transform target, float expandRadiusBy = 0f ) {
		List<GameObject> possibleTargets = WhatIsUnderFinger( finger, target.tag , expandRadiusBy);
		foreach( GameObject thisObject in possibleTargets ) {
			if( thisObject == target.gameObject ){
				return true;
			}
		}
		return false;
	}
}
