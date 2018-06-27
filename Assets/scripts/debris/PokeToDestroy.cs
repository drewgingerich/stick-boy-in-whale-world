using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeToDestroy : MonoBehaviour {

	[SerializeField] int hitsToDestroy = 3;

	int hitsTaken = 0;

	public void Hit() {
		hitsTaken++;
		if (hitsTaken >= hitsToDestroy)
			Destroy();
		// Handle hit animations
	}

	void Destroy() {
		Destroy(gameObject);
		// Handle destruction animations
	}
}