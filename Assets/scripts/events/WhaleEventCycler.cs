using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhaleEventCycler : MonoBehaviour {

	public static WhaleEventCycler instance;

	[SerializeField] List<WhaleEvent> whaleEvents;

	List<WhaleEvent> readyEvents;
	List<WhaleEvent> usedEvents;

	WhaleEvent activeEvent;

	void Awake() {
		Debug.Assert(instance == null);
		instance = this;
		readyEvents = whaleEvents;
		usedEvents = new List<WhaleEvent>();
	}

	void Start () {
		foreach (WhaleEvent whaleEvent in whaleEvents) {
			whaleEvent.OnSucceed.AddListener(FindNextEvent);
		}
	}

	public void FindNextEvent() {
		int randIndex = Random.Range(0, whaleEvents.Count);
		activeEvent = readyEvents[randIndex];
		UpdateEventTracking();
		activeEvent.StartEvent();
	}

	void UpdateEventTracking() {
		usedEvents.Add(activeEvent);
		readyEvents.Remove(activeEvent);
		if (readyEvents.Count == 0) {
			List<WhaleEvent> temp = readyEvents;
			readyEvents = usedEvents;
			usedEvents = temp;
		}
	}
}
