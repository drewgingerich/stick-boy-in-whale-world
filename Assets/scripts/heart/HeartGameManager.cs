﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HeartGameManager : MonoBehaviour {

	public UnityEvent OnSucceed;

	[Header("Balance")]
	[SerializeField] int patternLength = 3;
	[SerializeField] float blinkTime = 0.5f;
	[SerializeField] float betweenBlinkTime = 0.1f;

	[Header("UI Setup")]
	[SerializeField] Animator heartAnimator;

	[SerializeField] List<HeartChamber> heartChambers;
	[SerializeField] TriggerEventOnEnter demoTrigger;

	float hitAnimationTime = 0.3f;
	List<int> pattern;
	int patternIndex = 0;
	bool readyForHit;

	void Awake() {
		pattern = new List<int>();
	}

	void Start() {
		foreach(HeartChamber chamber in heartChambers) {
			chamber.OnHit += ChamberHit;
		}
	}

	public void OnKillWhale() {
		readyForHit = false;
		heartAnimator.SetBool("stopHeart", true);
		foreach (HeartChamber chamber in heartChambers) {
			chamber.OnHit -= ChamberHit;
		}
		demoTrigger.TurnOff();
	}

	public void Play() {
		heartAnimator.SetBool("stopHeart", true);
		GeneratePattern();
		demoTrigger.TurnOn();
	}

	void GeneratePattern() {
		pattern.Clear();
		for (int i = 0; i < patternLength; i++) {
			pattern.Add(Random.Range(0, heartChambers.Count));
		}
		patternIndex = 0;
	}

	public void DemoPattern() {
		StartCoroutine(DemoPatternRoutine());
	}

	IEnumerator DemoPatternRoutine() {
		readyForHit = false;
		foreach(int chamberIndex in pattern) {
			HeartChamber chamber = heartChambers[chamberIndex];
			int chamberPositionIndex = (int)chamber.position;
			heartAnimator.SetInteger("chamberPositionIndex", chamberPositionIndex);
			heartAnimator.SetBool("highlightChamber", true);
			yield return new WaitForSeconds(blinkTime);
			heartAnimator.SetBool("highlightChamber", false);
			yield return new WaitForSeconds(blinkTime);
		}
		readyForHit = true;
	}

	public void ChamberHit(HeartChamber chamber) {
		if (!readyForHit)
			return;
		int chamberIndex = heartChambers.IndexOf(chamber);
		if (chamberIndex == pattern[patternIndex])
			StartCoroutine(RegisterSuccessfulHit());
		else
			StartCoroutine(RegisterFailedHit());
	}

	IEnumerator RegisterSuccessfulHit() {
		readyForHit = false;
		heartAnimator.SetTrigger("goodHit");
		yield return new WaitForSeconds(hitAnimationTime);
		readyForHit = true;
		patternIndex++;
		if (patternIndex == pattern.Count)
			Succeed();
	}

	IEnumerator RegisterFailedHit() {
		readyForHit = false;
		heartAnimator.SetTrigger("badHit");
		yield return new WaitForSeconds(hitAnimationTime);
		readyForHit = true;
		patternIndex = 0;
		StartCoroutine(DemoPatternRoutine());
	}

	void Succeed() {
		heartAnimator.SetBool("stopHeart", false);
		demoTrigger.TurnOff();
		OnSucceed.Invoke();
	}
}
