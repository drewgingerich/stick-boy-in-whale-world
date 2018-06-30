using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StomachBoiGo : MonoBehaviour {

	[Header("Oh shit!")]
	[SerializeField] float acceleration = 30;
	[SerializeField] float maxVelocity = 20;
	[SerializeField] float friction = 2;
	[SerializeField] float glanceMultiplier;
	[SerializeField] float bounceMultiplier;
	
	[SerializeField] public StomachModel Stomach;

	Rigidbody2D rb2d;
	Vector2 inputDirection;
	Vector2 deltaVelocity;
	
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


	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	//	Stomach = GetComponent<StomachModel>();
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
	
	void OnCollisionEnter2D (Collision2D collision) {
		if (Stomach.Left <= x && x <= Stomach.Right && Stomach.Floor <= y && y <= Stomach.Roof)
		{	if (rb2d.velocity.magnitude < maxVelocity)
			{	Vector2 collisionNormal = collision.contacts[0].normal;
				Vector2 collisionReflection = Vector2.Reflect(rb2d.velocity, collisionNormal);
				Vector2 parallelComponent = Vector2.Dot(collisionNormal, collisionReflection) * collisionNormal;
				Vector2 perpendicularComponent = collisionReflection - parallelComponent;
				Vector2 bounceForce = perpendicularComponent * glanceMultiplier + parallelComponent * bounceMultiplier;
				rb2d.AddForce(bounceForce, ForceMode2D.Impulse);
			}
			else rb2d.velocity = maxVelocity * rb2d.velocity.normalized;
		}
	}
}
