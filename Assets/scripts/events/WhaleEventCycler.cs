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

	void Start () {
		foreach (WhaleEvent whaleEvent in availableEvents) {
			whaleEvent.OnSucceed.AddListener(FindNextEvent);
		}
	}

	public void FindNextEvent() {
		int randIndex = Random.Range(0, availableEvents.Count);
		activeEvent = availableEvents[randIndex];
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
