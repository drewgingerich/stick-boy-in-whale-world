using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Lung : MonoBehaviour {

	public event System.Action OnPoke = delegate { };

	[System.NonSerialized] public bool healthy = true;

	Animator animator;
	int breatheHash = Animator.StringToHash("Breathe");
	int weakBreatheHash = Animator.StringToHash("WeakBreathe");
	int struggleHash = Animator.StringToHash("Struggle");

	void Awake() {
		animator = GetComponent<Animator>();
	}

	public void Poke() {
		OnPoke();
	}

	public void Breath() {
		if (healthy)
			animator.SetTrigger(breatheHash);
	}

	public void WeakBreath() {
		animator.SetTrigger(weakBreatheHash);
	}

	public void Struggle() {
		animator.SetTrigger(struggleHash);
	}
}
