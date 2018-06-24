using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoiGo : MonoBehaviour {

	[Header("Oh shit!")]
	[SerializeField] float acceleration = 40;
	[SerializeField] float maxVelocity = 20;
	[SerializeField] float friction = 2;

	Rigidbody2D rb2d;
	Vector2 inputDirection;
	Vector2 deltaVelocity;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update() {
		UpdateInputDirection();
	}

	void FixedUpdate() {
		if (inputDirection != Vector2.zero)
			ApplyInputForce();
		ApplyFrictionForce();
		rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxVelocity);
	}

	void UpdateInputDirection() {
		inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
	}

	void ApplyInputForce() {
		Vector2 inputForce = inputDirection * acceleration;
		rb2d.AddForce(inputForce);
	}

	void ApplyFrictionForce() {
		if (rb2d.velocity.magnitude < friction * Time.fixedDeltaTime)
			rb2d.velocity = Vector2.zero;
		else
			rb2d.AddForce(-rb2d.velocity.normalized * friction);
	}
}
