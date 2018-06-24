using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

	public float timeBetweenHeartAttacks = 20f;
	public bool isRunning = true;
	public float healthLostPerSecond = 2f;
	public Collider2D heartRbody;

	[Header("DEBUG VISUALIZATION")]
	public float healthLeft = 20f;
	public Slider heartHealthSlider;
	

	// Use this for initialization
	void Start () {
		heartHealthSlider.maxValue = healthLeft;
	}

	public void StartHeart() {
		isRunning = true;
		StartCoroutine( heartAttackCountdown() );
	}

	public void StopHeart() {
		StartCoroutine( heartIsStoppedRoutine() );
	}

	/// Refers to Stick.cs
	public void OnHitByStick() {
		StartHeart();
	}
	
	// Update is called once per frame
	void Update () {
		heartHealthSlider.value = healthLeft;
	}

	IEnumerator heartAttackCountdown() { 
		for( float timeLeft = timeBetweenHeartAttacks; timeLeft > 0f; timeLeft -= Time.deltaTime ) {
			yield return null;
		}
		StopHeart();
		isRunning = false;
	}

	IEnumerator heartIsStoppedRoutine() {
		while( !isRunning ){
			healthLeft -= healthLostPerSecond * Time.deltaTime;
			yield return null;
		}
	}


}
