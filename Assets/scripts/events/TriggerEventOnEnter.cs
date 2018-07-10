using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerEventOnEnter : MonoBehaviour {

	public UnityEvent OnEnterTrigger;

	[SerializeField] bool on = true;

	void OnTriggerEnter2D(Collider2D other) {
		if (on)
			OnEnterTrigger.Invoke();
	}

	public void TurnOn() {
		on = true;
	}

	public void TurnOff() {
		on = false;
	}
}
