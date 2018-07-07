using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LungGame : MonoBehaviour {

	public UnityEvent OnSucceed;
	public UnityEvent OnFail;

	[SerializeField] Lung leftLung;
	[SerializeField] Lung rightLung;
	[SerializeField] float baseBreathTime = 0.75f;
	[SerializeField] float breathTimeVariationUnit = 0.25f;
	[SerializeField] int numberOfBreaths = 3;
	[SerializeField] float pokeGracePeriod = 0.1f;
	[SerializeField] float visualLagAdjustment = 0.15f;
	[SerializeField] float baseFailTime = 30f;

	Lung unhealthyLung;
	List<float> breathTimes;
	int breathIndex;
	float breathTimer;
	int successchain;
	Coroutine failTimer;

	void Awake() {
		breathTimes = new List<float>();
		GenerateBreathTimes();
	}

	public void Play() {
		GenerateBreathTimes();
		breathTimer = 0;
		breathIndex = 0;
		successchain = 0;
		SetUnhealthyLung();
		failTimer = StartCoroutine(FailTimerRoutine(baseFailTime));
	}

	public void OnKillWhale() {
		if (unhealthyLung != null)
			unhealthyLung.OnPoke -= OnPokeUnhealthyLung;
		leftLung.healthy = false;
		rightLung.healthy = false;
	}

	IEnumerator FailTimerRoutine(float failTime) {
		yield return new WaitForSeconds(failTime);
		OnFail.Invoke();
	}

	void GenerateBreathTimes() {
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
		StopCoroutine(failTimer);
		unhealthyLung.OnPoke -= OnPokeUnhealthyLung;
		unhealthyLung.healthy = true;
		unhealthyLung = null;
		OnSucceed.Invoke();
	}
}
