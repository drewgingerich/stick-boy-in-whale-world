using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleBoiGo : MonoBehaviour {

	[SerializeField] float speed = 10;

	Rigidbody2D rb2d;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update () {
		Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;		
		rb2d.velocity = (Vector3)inputDirection * speed;
	}
}
