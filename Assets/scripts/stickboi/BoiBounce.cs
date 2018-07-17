using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoiBounce : MonoBehaviour {

	[Header("What up?")]
	[SerializeField] float glanceMultiplier = 2;
	[SerializeField] float bounceMultiplier = 1.5f;

	Rigidbody2D rb2d;
	Vector2 velocity;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		velocity = rb2d.velocity;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Vector2 collisionNormal = collision.contacts[0].normal;
		Vector2 collisionReflection = Vector2.Reflect(velocity, collisionNormal);
		Vector2 parallelComponent = Vector2.Dot(collisionNormal, collisionReflection) * collisionNormal;
		Vector2 perpendicularComponent = collisionReflection - parallelComponent;
		Vector2 bounceForce = perpendicularComponent * glanceMultiplier + parallelComponent * bounceMultiplier;
		rb2d.AddForce(bounceForce, ForceMode2D.Impulse);
	}
}
