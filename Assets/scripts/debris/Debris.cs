using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Debris : MonoBehaviour {

	public event System.Action OnBreak;

	[SerializeField] float secondsToDestroy = 2f;
	[SerializeField] float chainPokeMinimumTime = 0.5f;
	[SerializeField] bool hideOnStart = true;

	float timeSinceLastHit;
	bool chainPoke;
	float timeChainPoked;
	bool pokeable = true;

	Animator animator;

	int spawnHash = Animator.StringToHash("Spawn");
	int wobbleHash = Animator.StringToHash("Wobble");
	int breakHash = Animator.StringToHash("Break");

	void Awake() {
		if (hideOnStart)
			gameObject.SetActive(false);
	}

	void Update() {
		timeSinceLastHit += Time.deltaTime;
	}

	public void Spawn() {
		gameObject.SetActive(true);
		timeSinceLastHit = 0;
		timeChainPoked = 0;
		// animator.SetTrigger(spawnHash);
	}
	
	public void Poke() {
		if (!pokeable)
			return;
		chainPoke = timeSinceLastHit <= chainPokeMinimumTime ? true : false;
		if (chainPoke) {
			timeChainPoked += Time.deltaTime;
			// animator.SetTrigger(wobbleHash);
		}
		if (timeChainPoked >= secondsToDestroy)
			StartCoroutine(BreakRoutine());
		timeSinceLastHit = 0;
	}

	IEnumerator BreakRoutine() {
		pokeable = false;
		// animator.SetTrigger(breakHash);
		yield return new WaitForSeconds( .01f );
		pokeable = true;
		OnBreak();
		gameObject.SetActive(false);
	}
}
