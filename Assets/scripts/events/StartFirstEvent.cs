using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFirstEvent : MonoBehaviour {

	[SerializeField] Dialogue startingDialogue;
	[SerializeField] EventTracker startingEvent;

	void Awake() {
		startingDialogue.OnDialogueFinish += () => startingEvent.StartEvent();
	}
}
