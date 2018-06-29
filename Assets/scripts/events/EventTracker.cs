using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTracker : MonoBehaviour {

	public UnityEvent OnStartEvent;
	public UnityEvent OnEndEvent;

	public void StartEvent() {
		OnStartEvent.Invoke();
	}

	public bool IsReady() {
		return true;
	}

	public void FinishEvent() {
		// EventManager.instance.FindNextEvent();
		OnEndEvent.Invoke();
	}
}
