using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebrisGroup : MonoBehaviour {

	public UnityEvent OnFail;
	public UnityEvent OnSucceed;

	public List<Debris> debrisList;
	public float baseTimeLimit;
	public float minTimeLimit;
	public float difficultySubtraction;

	Coroutine countdownRoutine;

	void Start() {
		foreach (Debris debris in debrisList) {
			debris.OnBreak += EndMinigame;
		}
	}

	public void StartMinigame(int difficulty) {
		int randomIndex = Random.Range(0, debrisList.Count);
		Debris activeDebris = debrisList[randomIndex];
		activeDebris.Spawn();
		float timeLimit = baseTimeLimit - difficultySubtraction * difficulty;
		timeLimit = Mathf.Clamp(timeLimit, minTimeLimit, float.PositiveInfinity);
		countdownRoutine = StartCoroutine(CountdownRoutine(timeLimit));
	}

	IEnumerator CountdownRoutine(float time) {
		yield return new WaitForSeconds(time);
		OnFail.Invoke();
	}

	public void EndMinigame() {
		StopCoroutine(countdownRoutine);
		OnSucceed.Invoke();
	}
}
