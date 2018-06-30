using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves gameobject in localspace to target. Uses Update frame-timing, not fixedupdate.
/// </summary>
public class SlideToPosition : MonoBehaviour {

	public bool moveOnStart = false;
	public bool startAtDestination;
	Vector3 originalPosition;
	public Vector3 localTargetTranslation;
	public float duration;
	public AnimationCurve animationCurve;
	[SerializeField] bool isMoving = false;
	[SerializeField] bool headingToDestination = false;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
	}

	public void Toggle() {
		StopAllCoroutines();
		StartCoroutine(AnimateToDestination());
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos() {
		Gizmos.DrawLine( transform.position, originalPosition + localTargetTranslation );
		Gizmos.DrawWireCube( originalPosition + localTargetTranslation, Vector3.one * .2f );
	}

	IEnumerator AnimateToDestination() {
		Vector3 start = transform.localPosition; // Worldspace
		Vector3 destination; // Worldspace
		isMoving = true;
		if( headingToDestination ) {
			destination = originalPosition + localTargetTranslation;
		} else {
			destination = originalPosition;
		}
		for( float time = 0f; time < duration; time += Time.deltaTime ) {
			// added support for "overshooting" your target
			Vector3 newPosition = Vector3.LerpUnclamped(
				start,
				destination,
				animationCurve.Evaluate( time / duration )
			);
			transform.localPosition = newPosition;
			yield return null;
		}
		transform.localPosition = destination;
		isMoving = false;
	}
}
