using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickInteractable : MonoBehaviour {

	public UnityEvent OnHit;

	[SerializeField] GameObject OnHitSparks;

	void Awake() {
		gameObject.layer = LayerMask.NameToLayer("Stick");
	}

	public void Hit(RaycastHit2D hit) {
		if( OnHitSparks != null ) {
			Instantiate(OnHitSparks, hit.point, OnHitSparks.transform.rotation, transform);
		}
		OnHit.Invoke();
	}
}
