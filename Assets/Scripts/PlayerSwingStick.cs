using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingStick : MonoBehaviour {
	// public enum StickInteractionType{ Poke, Swing, LAST };


	[Header("Current State")]
	[SerializeField] bool isSwinging = false;
	// public StickInteractionType currentStickInteraction;

	[Header("Setup")]
	public GameObject stick;
	// private float stickExpandedSize;
	public float swingTime;

	void Start() {
		isSwinging = false;
		stick.SetActive(false);
	}

	void Update() {
		if( Input.GetAxis("Fire1") > 0 ) {
			SwingStick();
		}
	}

	public void SwingStick() {
		if( !isSwinging ){
			// Physics2D.CircleCast()
			StartCoroutine( StickIsSwinging() );
		}
	}

	/// <summary>
	/// Just the animation for swinging the stick
	/// </summary>
	IEnumerator StickIsSwinging() {
		isSwinging = true;
		stick.SetActive(true);
		for( float timeLeft = swingTime; timeLeft > 0; timeLeft -= Time.deltaTime ) {
			yield return null;

		}
		stick.SetActive(false);
		isSwinging = false;
	}
}
