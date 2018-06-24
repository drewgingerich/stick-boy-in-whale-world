using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingStick : MonoBehaviour {
	public GameObject stick;
	// private float stickExpandedSize;
	public float swingTime;
	private bool isSwinging = false;

	void Start() {
		isSwinging = false;
		stick.SetActive(false);
	}

	void Update() {
		if( Input.GetAxis("Fire1") > 0 ) {
			SwingStick();
		}
	}

	void SwingStick() {
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
