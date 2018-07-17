using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySparks : MonoBehaviour {

	public float lifespan = .5f;

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds( lifespan);
		Destroy(gameObject);
	}

}
