using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventRepeater : MonoBehaviour {

	public UnityEvent OnRepeatEvent;

	public float repeatDelay = 5f;

	public void RepeatEvent() {
		StartCoroutine(RepeatEventRoutine());
	}

	IEnumerator RepeatEventRoutine() {
		yield return new WaitForSeconds(repeatDelay);
		OnRepeatEvent.Invoke();
	}
}
