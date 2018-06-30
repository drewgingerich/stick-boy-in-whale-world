using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Debris : MonoBehaviour {

	public event System.Action OnBreak;
	[SerializeField] float secondsToDestroy = 2f;
	[SerializeField] float chainPokeMinimumTime = 0.2f;

	float timeSinceLastHit;
	bool chainPoke;
	float timeChainPoked;

	Animator animator;

	int spawnHash = Animator.StringToHash("Spawn");
	int wobbleHash = Animator.StringToHash("Wobble");
	int breakHash = Animator.StringToHash("Break");

	void Awake() {
		gameObject.SetActive(false);
	}

	void Update() {
		timeSinceLastHit += Time.deltaTime;
	}

	public void Spawn() {
		gameObject.SetActive(true);
		timeSinceLastHit = 0;
		animator.SetTrigger(spawnHash);
	}
	
	public void Poke() {
		if (timeSinceLastHit <= chainPokeMinimumTime)
			chainPoke = true;
		if (chainPoke) {
			timeChainPoked += Time.deltaTime;
			animator.SetTrigger(wobbleHash);
		}
		if (timeChainPoked >= secondsToDestroy)
			Break();
		timeSinceLastHit = 0;
	}

	public void Break() {
		animator.SetTrigger(breakHash);
		StartCoroutine(BreakRoutine());
	}

	IEnumerator BreakRoutine() {
		yield return new WaitForSeconds(2);
		gameObject.SetActive(false);
		EventManager.instance.FindNextEvent();
	}
}
