using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebrisGroup : MonoBehaviour {

	public UnityEvent OnSucceed;

	public List<Debris> debrisList;
	[SerializeField] int startingDebrisNumber = 1;

	int difficulty;

	void Awake() {
		difficulty = startingDebrisNumber;
	}

	void Start() {
		foreach (Debris debris in debrisList) {
			debris.OnBreak += EndMinigame;
			debris.gameObject.SetActive(false);
		}
	}

	public void OnKillWhale() {
		foreach (Debris debris in debrisList) {
			debris.OnBreak -= EndMinigame;
		}
	}

	public void StartMinigame() {
		List<Debris> inactiveDebris = new List<Debris>(debrisList);
		for (int i = 0; i < difficulty; i++) {
			int randomIndex = Random.Range(0, inactiveDebris.Count);
			inactiveDebris[randomIndex].Spawn();
			inactiveDebris.RemoveAt(randomIndex);
		}
	}

	void EndMinigame() {
		if (difficulty < debrisList.Count - 1)
			difficulty++;
		OnSucceed.Invoke();
	}
}
