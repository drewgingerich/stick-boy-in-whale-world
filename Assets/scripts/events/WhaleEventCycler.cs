using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhaleEventCycler : MonoBehaviour {

	public static WhaleEventCycler instance;

	[SerializeField] List<WhaleEvent> availableEvents;

	List<WhaleEvent> usedEvents;

	WhaleEvent activeEvent;

	void Awake() {
		Debug.Assert(instance == null);
		instance = this;
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
