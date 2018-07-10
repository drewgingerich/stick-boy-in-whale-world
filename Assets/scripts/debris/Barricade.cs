using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StickInteractable))]
public class Barricade : MonoBehaviour {

	StickInteractable interactable;

	void Awake() {
		interactable = GetComponent<StickInteractable>();
	}

	public void Start() {
		interactable.enabled = false;
	}

	public void MakeBreakable() {
		interactable.enabled = true;
	}
}
