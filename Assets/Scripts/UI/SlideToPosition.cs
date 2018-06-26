using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves gameobject in localspace to target. Uses frame-timing, not fixedupdate.
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
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Toggle() {
		StopAllCoroutines();
		StartCoroutine(AnimateToDestination());
	}

	IEnumerator AnimateToDestination() {
		Vector3 start = transform.position; // Worldspace
		Vector3 destination; // Worldspace
		if( headingToDestination ) {
			destination = originalPosition + localTargetTranslation;
		} else {
			destination = originalPosition;
		}
		for( float time = 0f; time < duration; time += Time.deltaTime ) {
			
			yield return null;
		}
	}
}
