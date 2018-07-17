// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// // [RequireComponent(typeof(SpriteRenderer))]
// public class ShakeOnCommand : MonoBehaviour {

// 	[Header("Behavior")]
// 	public bool shakeOnStart = false;
// 	public float defaultShakeDuration = 1f;
// 	// public Color highlightedColor = Color.white;
// 	public float shakeAmountModifier = 1f;
// 	public AnimationCurve shakeCurve;
// 	bool shaking = false;
// 	// SpriteRenderer spriteRenderer;
// 	Quaternion originalLocalRotation;
// 	// Color originalColor;

// 	// Use this for initialization
// 	void Start () {
// 		// spriteRenderer = GetComponent<SpriteRenderer>();
// 		if( shakeOnStart ) 
// 			Shake( defaultShakeDuration );
// 	}

// 	/// <summary>
// 	/// Tells the object to start shaking
// 	/// </summary>
// 	/// <param name="duration"></param>
// 	public void Shake( float duration = 0f) {
// 		if( duration == 0f)
// 			duration = defaultShakeDuration;
// 		if( !shaking )
// 			StartCoroutine(ShakeRoutine( duration ));
// 	}

// 	/// <summary>
// 	/// Shakes the object just once.
// 	/// 
// 	/// **WARNING:** Will leave the object ajar so make sure to call <see cref="ShakeOnCommand.Reset" /> to reset
// 	/// </summary>
// 	public void ShakeOnce( float amount = 0f) {
// 		if( !shaking ) {
// 			originalLocalRotation = transform.localRotation;
// 			// originalColor = spriteRenderer.color;
// 		}
// 		shaking = true;
// 		if( amount == 0f ){
// 			amount = shakeAmountModifier;
// 		}
// 		ImplementShake( amount );
// 	}

// 	public void Reset() {
// 		transform.localRotation = originalLocalRotation;
// 		// spriteRenderer.color = originalColor;
// 		shaking = false;
// 	}

// 	IEnumerator ShakeRoutine( float duration ) {
// 		shaking = true;
// 		Quaternion originalRotation = transform.localRotation;
// 		// Color originalColor = spriteRenderer.color;
// 		// spriteRenderer.color = highlightedColor;
// 		for( float timeLeft = duration; timeLeft > 0f; timeLeft -= Time.deltaTime ) {
// 			float currentShakeAmount = shakeCurve.Evaluate( (duration - timeLeft) / duration ) * shakeAmountModifier;
// 			ImplementShake( currentShakeAmount );
// 			yield return null;
// 		}
// 		transform.localRotation = originalRotation;
// 		// spriteRenderer.color = originalColor;
// 		shaking = false;
// 	}

// 	void ImplementShake( float currentShakeAmount ) {
// 		// Stolen from http://wiki.unity3d.com/index.php/Camera_Shake
// 		Vector3 rotationAmount = Random.insideUnitSphere * currentShakeAmount;
// 		rotationAmount.x = rotationAmount.y = 0; // Only rotate on Z axis
// 		transform.localRotation = Quaternion.Euler (rotationAmount);
// 	}
// }
