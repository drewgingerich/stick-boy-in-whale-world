using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShakeOnCommand))]
[RequireComponent(typeof(StickInteractable))]
public class HeartChamber : MonoBehaviour {

	ShakeOnCommand shaker;
	public bool interactable;
	// public AnimationState
	// public string chamberHighlightStateName;
	// [HideInInspector] public int highlightAnimHash;

	void Start() {
		shaker = GetComponent<ShakeOnCommand>();
		// highlightAnimHash = 
	}
	 
	public void Shake(float duration ) {
		shaker.Shake( duration );
	}
}
