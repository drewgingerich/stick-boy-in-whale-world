using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDelay : MonoBehaviour {

	public UnityEvent OnSendEvent;

	public float delay = 5f;

	public void DelayEvent() {
		StartCoroutine(DelayEventRoutine());
	}

	IEnumerator DelayEventRoutine() {
		yield return new WaitForSeconds(delay);
		OnSendEvent.Invoke();
	}
}
