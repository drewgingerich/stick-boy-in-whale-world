using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebrisGroup : MonoBehaviour {

	public UnityEvent OnSucceed;

	public List<Debris> debrisList;

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
	}

	void EndMinigame() {
		OnSucceed.Invoke();
	}
}
