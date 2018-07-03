using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Debris : MonoBehaviour {

	public event System.Action OnBreak;

	[SerializeField] float secondsToDestroy = 2f;
	[SerializeField] float chainPokeMinimumTime = 0.2f;
	[SerializeField] bool hideOnStart = true;

	float timeSinceLastHit;
	bool chainPoke;
	float timeChainPoked;

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
		// animator.SetTrigger(spawnHash);
	}
	
	public void Poke() {
		// if (timeSinceLastHit <= chainPokeMinimumTime)
		// 	chainPoke = true;
		// if (chainPoke) {
		// 	timeChainPoked += Time.deltaTime;
		// 	animator.SetTrigger(wobbleHash);
		// }
		// if (timeChainPoked >= secondsToDestroy)
			StartCoroutine(BreakRoutine());
		timeSinceLastHit = 0;
	}

	IEnumerator BreakRoutine() {
		// animator.SetTrigger(breakHash);
		yield return new WaitForSeconds( .01f );
		OnBreak();
		gameObject.SetActive(false);
	}
}
