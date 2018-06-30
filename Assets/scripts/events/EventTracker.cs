using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTracker : MonoBehaviour {

	public UnityEvent OnStartEvent;
	public UnityEvent OnEventSucceed;
	public UnityEvent OnEventFail;

	public void StartEvent() {
		OnStartEvent.Invoke();
	}

	public bool IsReady() {
		return true;
	}

	public void EventSucceed() {
		// EventManager.instance.FindNextEvent();
		OnEventSucceed.Invoke();
	}

	public void EventFail() {
		OnEventFail.Invoke();
	}
}
