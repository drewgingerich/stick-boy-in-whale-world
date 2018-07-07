using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerEventOnEnter : MonoBehaviour {

	public UnityEvent OnEnterTrigger;

	void OnTriggerEnter2D(Collider2D other) {
		OnEnterTrigger.Invoke();
	}
}
