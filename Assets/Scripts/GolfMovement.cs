using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfMovement : MonoBehaviour {
	
	Vector3 dbg_target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Touch touch = Input.GetTouch(0);
		
	}

	/// <summary>
	/// Routine for dragging a golf swing
	/// </summary>
	/// <returns></returns>
	IEnumerator GolfSwingRoutine() {
		yield return null;
	}
}
