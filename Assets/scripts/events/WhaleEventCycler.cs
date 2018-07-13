using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WhaleEventCycler : MonoBehaviour {

	public UnityEvent OnStartNextEvent;

	[SerializeField] List<WhaleEvent> availableEvents;

	List<WhaleEvent> usedEvents;

	WhaleEvent activeEvent;

	void Start() {
		usedEvents = new List<WhaleEvent>();
	}

	public void StartRandomEvent() {
		int randIndex = Random.Range(0, availableEvents.Count);
		WhaleEvent whaleEvent = availableEvents[randIndex];
		StartEvent(whaleEvent);
	}

	public void StartEvent(WhaleEvent whaleEvent) {
		activeEvent = whaleEvent;
		if (availableEvents.Contains(whaleEvent))
			UpdateEventTracking();
		activeEvent.StartEvent();
		OnStartNextEvent.Invoke();
		Debug.Log(activeEvent);
	}

	void UpdateEventTracking() {
		usedEvents.Add(activeEvent);
		availableEvents.Remove(activeEvent);
		if (availableEvents.Count == 0) {
			List<WhaleEvent> temp = availableEvents;
			availableEvents = usedEvents;
			usedEvents = temp;
		}
	}
}
