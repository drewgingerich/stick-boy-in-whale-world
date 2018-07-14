using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LungGame : MonoBehaviour {

	public UnityEvent OnSucceed;

	[SerializeField] Lung leftLung;
	[SerializeField] Lung rightLung;
	[SerializeField] float baseBreathTime = 0.75f;
	[SerializeField] float breathTimeVariationUnit = 0.25f;
	[SerializeField] int startingBreathCount = 1;
	[SerializeField] float pokeGracePeriod = 0.1f;
	[SerializeField] float visualLagAdjustment = 0.15f;

	Lung unhealthyLung;
	List<float> breathTimes;
	int breathIndex;
	float breathTimer;
	int successchain;
	int difficulty = 0;

	void Awake() {
		difficulty = startingBreathCount;
		breathTimes = new List<float>();
		GenerateBreathTimes(difficulty);
	}

	public void Play() {
		GenerateBreathTimes(difficulty);
		breathTimer = 0;
		breathIndex = 0;
		successchain = 0;
		SetUnhealthyLung();
	}

	public void OnKillWhale() {
		if (unhealthyLung != null)
			unhealthyLung.OnPoke -= OnPokeUnhealthyLung;
		leftLung.healthy = rightLung.healthy = false;
	}

	void GenerateBreathTimes(int numberOfBreaths) {
		breathTimes.Clear();
		for (int i = 0; i < numberOfBreaths; i++) {
			breathTimes.Add(baseBreathTime + Random.Range(0, 2) * breathTimeVariationUnit);
		}
	}

	void SetUnhealthyLung() {
		unhealthyLung = Random.value < 0.5f ? rightLung : leftLung;
		unhealthyLung.healthy = false;
		unhealthyLung.OnPoke += OnPokeUnhealthyLung;
	}

	void Update() {
		breathTimer += Time.deltaTime;
		if (breathTimer >= breathTimes[breathIndex]) {
			leftLung.Breath();
			rightLung.Breath();
			SetupNextBreath();
		}
	}

	void SetupNextBreath() {
		breathTimer = breathTimes[breathIndex] - breathTimer;
		breathIndex++;
		breathIndex %= breathTimes.Count;
	}

	void OnPokeUnhealthyLung() {
		float adjustedTime = breathTimer - visualLagAdjustment;
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
		unhealthyLung.OnPoke -= OnPokeUnhealthyLung;
		unhealthyLung.healthy = true;
		unhealthyLung = null;
		difficulty++;
		OnSucceed.Invoke();
	}
}
