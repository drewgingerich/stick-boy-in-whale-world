using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezeStretch : MonoBehaviour {

	#region Transform Shortcuts
	private float x { get {return transform.position.x;} set {transform.position = new Vector3(value, transform.position.y, transform.position.z);} }
	private float y { get {return transform.position.y;} set {transform.position = new Vector3(transform.position.x, value, transform.position.z);} }
	private float z { get {return transform.position.z;} set {transform.position = new Vector3(transform.position.x, transform.position.y, value);} }
	private float xo{ get {return transform.rotation.x;} /*set {transform.Rotate(value, transform.rotation.y, transform.rotation.z);}*/ }
	private float yo{ get {return transform.rotation.y;} /*set {transform.Rotate(transform.rotation.x, value, transform.rotation.z);}*/ }
	private float zo{ get {return transform.rotation.z;} /*set {transform.Rotate(transform.rotation.x, transform.rotation.y, value);}*/ }
	private float xl{ get {return transform.localScale.x;} set {transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);} }
	private float yl{ get {return transform.localScale.y;} set {transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);} }
	private float zl{ get {return transform.localScale.z;} set {transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);} }
	#endregion

	void Start () {
		
	}
	
	void Update () {
		const int MAG = 3; //magnitude
		int sign = 1;
		if ( (yl >= MAG && sign < 0) || (xl >= MAG && sign > 0) ) sign *= -1;  //switch the sign whenever the extents are reached
		
		xl += 0.2f*sign*Time.deltaTime;
		yl -= 0.2f*sign*Time.deltaTime;
	}
}
