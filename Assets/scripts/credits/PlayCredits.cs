using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PlayCredits : MonoBehaviour {

	[SerializeField] float speed = 10f;
	[SerializeField] float boostMultiplier = 2f;

	[SerializeField] bool boost;
	RectTransform rectT;

	void Awake() {
		rectT = GetComponent<RectTransform>();
	}

	IEnumerator Start () {
		yield return new WaitForSeconds(2);
		while (true) {
			Vector2 offsetChange = Vector2.up * Time.deltaTime * speed;
			if (boost)
				offsetChange *= boostMultiplier;
			rectT.offsetMax += offsetChange;
			rectT.offsetMin += offsetChange;
			if (rectT.offsetMax.y > rectT.sizeDelta.y)
				break;
			else
				yield return null;
		}
	}

	public void StartBoost() {
		boost = true;
	}

	public void StopBoost() {
		boost = false;
	}
}
