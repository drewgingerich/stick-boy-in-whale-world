using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebrisGroup : MonoBehaviour {

	public UnityEvent OnSucceed;
	public UnityEvent OnFail;

	public List<Debris> debrisList;
	[SerializeField] float baseFailTime = 30f;

	Coroutine failTimer;

	void Start() {
		foreach (Debris debris in debrisList) {
			debris.OnBreak += EndMinigame;
		}
	}

	public void OnKillWhale() {
		foreach (Debris debris in debrisList) {
			debris.OnBreak -= EndMinigame;
		}
	}

	public void StartMinigame() {
		int randomIndex = Random.Range(0, debrisList.Count);
		Debris activeDebris = debrisList[randomIndex];
		activeDebris.Spawn();
		failTimer = StartCoroutine(FailTimerRoutine(baseFailTime));
	}

	IEnumerator FailTimerRoutine(float failTime) {
		yield return new WaitForSeconds(failTime);
		OnFail.Invoke();
	}

	void EndMinigame() {
		StopCoroutine(failTimer);
		OnSucceed.Invoke();
	}
}
