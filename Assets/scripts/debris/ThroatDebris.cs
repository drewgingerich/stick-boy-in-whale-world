using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThroatDebris : MonoBehaviour {

	public UnityEvent OnSucceed;

	[SerializeField] Debris debris;
	[SerializeField] int startingDifficulty = 1;
	[SerializeField] int difficultyMultiplier = 3;

	int difficulty;

	void Awake() {
		difficulty = startingDifficulty;
	}

	void Start() {
		debris.OnBreak += Succeed;
		debris.gameObject.SetActive(false);
	}

	public void StartMinigame() {
		debris.Spawn(difficulty * difficultyMultiplier);
	}

	void Succeed() {
		OnSucceed.Invoke();
	}

	public void OnKillWhale() {
		debris.OnBreak -= Succeed;
	}
}
