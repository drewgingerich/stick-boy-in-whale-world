using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WhaleEventFailMonitor : MonoBehaviour {

	public UnityEvent OnFailEvent;

	[SerializeField] List<WhaleEvent> failableEvents;

	void Start() {
		foreach (WhaleEvent failableEvent in failableEvents) {
			failableEvent.OnFail.AddListener(DetectEventFail);
		}
	}

	void DetectEventFail() {
		OnFailEvent.Invoke();
	}
}
