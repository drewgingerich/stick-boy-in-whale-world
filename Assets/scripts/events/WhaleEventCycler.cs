using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleEventCycler : MonoBehaviour {

	public static WhaleEventCycler instance;

	[SerializeField] List<WhaleEvent> trackers;

	WhaleEvent activeEventTracker;

	void Awake() {
		if (instance != null)
			Debug.LogError("Mulitple event manager instances.");
		instance = this;
	}

	public void FindNextEvent() {
		while (activeEventTracker == null) {
			int randIndex = Random.Range(0, trackers.Count);
			if (trackers[randIndex].IsReady())
				activeEventTracker = trackers[randIndex];
		}
		activeEventTracker.StartEvent();
	}
}
