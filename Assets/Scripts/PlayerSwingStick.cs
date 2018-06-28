using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingStick : MonoBehaviour {
	// public enum StickInteractionType{ Poke, Swing, LAST };

	[Header("Balance")]
	[SerializeField] float maxTapDistance = 5f;
	[SerializeField] float expandTouchRadiusBy = .5f;

	[Header("Current State")]
	[SerializeField] bool isSwinging = false;
	// public StickInteractionType currentStickInteraction;

	[Header("Setup")]
	public GameObject stick;
	// private float stickExpandedSize;
	public float swingTime;
	// [SerializeField] GameObject stickHitParticles;
	// [SerializeField] AnimationCurve debugStickAnimation;

	void Start() {
		isSwinging = false;
		stick.SetActive(false);
	}

	void Update() {
		if( Input.touchCount > 0 ) {
			// check if touch is close enough
			SwingStick();
		}
	}
	

	public void SwingStick() {
		if( !isSwinging ){
			Debug.Log("Trying to swing stick!");
			RaycastHit2D hit = GetFirstObjHitByStick( Input.touches[0] );
			if( hit ) {
				hit.collider.GetComponent<StickInteractable>().HitByStick( hit );
				
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
		if( hit.collider.GetComponent<StickInteractable>() == null ) {
			return new RaycastHit2D();
		} else {
			return hit;
		}
	}


	/// <summary>
	/// Just the animation for swinging the stick
	/// </summary>
	IEnumerator StickSwingRoutine() {
		isSwinging = true;
		stick.SetActive(true);
		for( float timeLeft = swingTime; timeLeft > 0; timeLeft -= Time.deltaTime ) {
			DebugAnimateStick( timeLeft );
			yield return null;

		}
		stick.SetActive(false);
		isSwinging = false;
	}

	void DebugAnimateStick( float timeLeft ) {

	}

	
}
