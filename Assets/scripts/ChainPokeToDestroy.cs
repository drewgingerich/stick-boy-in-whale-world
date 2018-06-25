using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPokeToDestroy : MonoBehaviour {

	[SerializeField] float secondsToDestroy = 2f;
	[SerializeField] float chainPokeMinimumTime = 0.2f;

	float timeSinceLastHit;
	bool chainPoke;
	float timeChainPoked;

	void Update() {
		timeSinceLastHit += Time.deltaTime;
	}
	public void Hit() {
		if (timeSinceLastHit <= chainPokeMinimumTime)
			chainPoke = true;
		if (chainPoke)
			timeChainPoked += Time.deltaTime;
		if (timeChainPoked >= secondsToDestroy)
			Destroy();
		timeSinceLastHit = 0;
		// Handle hit animations
	}

	void Destroy() {
		Destroy(gameObject);
		// Handle destruction animations
	}

}
