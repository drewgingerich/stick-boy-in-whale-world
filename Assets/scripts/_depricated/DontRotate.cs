// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DontRotate : MonoBehaviour {

// 	// Use this for initialization
// 	public bool useOriginalRotation;
// 	Quaternion targetRotation;
// 	void Start () {
// 		if( useOriginalRotation ) {
// 			targetRotation = transform.rotation;
// 		} else {
// 			targetRotation = Quaternion.identity;
// 		}
// 	}
	
// 	// Update is called once per frame
// 	void FixedUpdate () {
// 		transform.rotation = targetRotation;
// 	}
// }
