using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShakeOnCommand))]
[RequireComponent(typeof(StickInteractable))]
public class HeartChamber : MonoBehaviour {

	ShakeOnCommand shaker;
	public bool interactable;

	void Start() {
		shaker = GetComponent<ShakeOnCommand>();
	}
	 
	public void Shake(float duration ) {
		shaker.Shake( duration );
	}
}
