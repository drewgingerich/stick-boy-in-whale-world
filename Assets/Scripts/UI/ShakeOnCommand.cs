using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnCommand : MonoBehaviour {

	public Color highlightedColor;
	public float shakeAmountModifier = 1f;
	public AnimationCurve shakeCurve;
	bool shaking = false;

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// Tells the object to start shaking
	/// </summary>
	/// <param name="duration"></param>
	public void Shake( float duration = 1f) {
		if( !shaking )
			StartCoroutine(ShakeChamber( duration ));
	}

	IEnumerator ShakeChamber( float duration ) {
		shaking = true;
		Quaternion originalRotation = transform.rotation;
		Color originalColor = spriteRenderer.color;
		spriteRenderer.color = highlightedColor;
		for( float timeLeft = duration; timeLeft > 0f; timeLeft -= Time.deltaTime ) {
			float currentShakeAmount = shakeCurve.Evaluate( (duration - timeLeft) / duration ) * shakeAmountModifier;
			// Stolen from http://wiki.unity3d.com/index.php/Camera_Shake
			Vector3 rotationAmount = Random.insideUnitSphere * currentShakeAmount;
			rotationAmount.x = rotationAmount.y = 0; // Only rotate on Z axis
			transform.localRotation = Quaternion.Euler (rotationAmount);
			yield return null;
		}
		transform.rotation = originalRotation;
		spriteRenderer.color = originalColor;
		shaking = false;
	}
}
