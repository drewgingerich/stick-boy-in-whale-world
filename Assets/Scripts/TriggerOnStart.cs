using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnStart : MonoBehaviour {
    public UnityEvent target;

	// Use this for initialization
	void Start () {
		target.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
