using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LungGame : MonoBehaviour {

	public UnityEvent OnSucceed;

	[SerializeField] Lung leftLung;
	[SerializeField] Lung rightLung;
	[SerializeField] float baseBreathTime = 0.75f;
	[SerializeField] float breathTimeVariationUnit = 0.25f;
	[SerializeField] int numberOfBreaths = 3;
	[SerializeField] float pokeGracePeriod = 0.1f;
	[SerializeField] float visualLagAdjustment = 0.15f;

	Lung healthyLung;
	Lung unhealthyLung;
	bool playingMinigame;
	List<float> breathTimes;
	int breathIndex;
	float timer;
	int successchain;

	void Awake() {
		breathTimes = new List<float>();
		GenerateBreathTimes();
	}

	public void StartGame() {
		GenerateBreathTimes();
		timer = 0;
		breathIndex = 0;
		playingMinigame = true;
		DetermineUnhealthyLung();
		unhealthyLung.OnPoke += OnPokeUnhealthyLung;
	}

	void GenerateBreathTimes() {
		breathTimes.Clear();
		for (int i = 0; i < numberOfBreaths; i++) {
			breathTimes.Add(baseBreathTime + Random.Range(0, 2) * breathTimeVariationUnit);
		}
	}

	void DetermineUnhealthyLung() {
		if (Random.value < 0.5f) {
			healthyLung = rightLung;
			unhealthyLung = leftLung;
		} else {
			healthyLung = leftLung;
			unhealthyLung = rightLung;
		}
	}

	void Update() {
		if (playingMinigame)
			MinigameUpdate();
		else
			HealthyUpdate();
	}

	void HealthyUpdate() {
		timer += Time.deltaTime;
		if (timer >= breathTimes[breathIndex]) {
			HealthyBreath();
			SetupNextBreath();
		}
	}

	void MinigameUpdate() {
		timer += Time.deltaTime;
		if (timer >= breathTimes[breathIndex]) {
			UnhealthyBreath();
			SetupNextBreath();
		}
	}

	void SetupNextBreath() {
		timer = breathTimes[breathIndex] - timer;
		breathIndex++;
		breathIndex %= breathTimes.Count;
	}

	void HealthyBreath() {
		leftLung.Breath();
		rightLung.Breath();
	}

	void UnhealthyBreath() {
		healthyLung.Breath();
	}

	void OnPokeUnhealthyLung() {
		float adjustedTime = timer - visualLagAdjustment;
		if (adjustedTime < pokeGracePeriod || breathTimes[breathIndex] - adjustedTime < pokeGracePeriod)
			OnGoodPoke();
		else
			OnBadPoke();
	}

	void OnGoodPoke() {
		unhealthyLung.WeakBreath();
		successchain++;
		if (successchain >= breathTimes.Count)
			EndMinigame();
	}

	void OnBadPoke() {
		unhealthyLung.Struggle();
		successchain = 0;
	}

	void EndMinigame() {
		OnSucceed.Invoke();
		playingMinigame = false;
		unhealthyLung.OnPoke -= OnPokeUnhealthyLung;
	}
}
