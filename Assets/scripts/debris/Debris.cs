using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Debris : MonoBehaviour {

	public event System.Action OnBreak = delegate { };

	[SerializeField] int hitsToDestroy = 1;

	bool pokeable = true;
	int hitsTaken = 0;

	Animator animator;

	int spawnHash = Animator.StringToHash("Spawn");
	int wobbleHash = Animator.StringToHash("Wobble");
	int breakHash = Animator.StringToHash("Break");

	public void SetPokeable(bool value) {
		pokeable = value;
	}

	public void Spawn() {
		gameObject.SetActive(true);
		hitsTaken = 0;
		// animator.SetTrigger(spawnHash);
	}
	
	public void Spawn(int hitsToDestroy) {
		this.hitsToDestroy = hitsToDestroy;
		Spawn();
	}

	public void Poke() {
		if (!pokeable)
			return;
		hitsTaken++;
		if (hitsTaken == hitsToDestroy)
			StartCoroutine(BreakRoutine());
	}

	IEnumerator BreakRoutine() {
		pokeable = false;
		// animator.SetTrigger(breakHash);
		yield return new WaitForSeconds(0.01f);
		pokeable = true;
		OnBreak();
		gameObject.SetActive(false);
	}
}
