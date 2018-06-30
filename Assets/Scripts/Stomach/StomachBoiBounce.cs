using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StomachBoiBounce : MonoBehaviour {

	[Header("What up?")]
	[SerializeField] float glanceMultiplier;
	[SerializeField] float bounceMultiplier;
	[SerializeField] float maximum_velocity;

	Rigidbody2D rb2d;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (rb2d.velocity.magnitude < maximum_velocity)
		{	Vector2 collisionNormal = collision.contacts[0].normal;
			Vector2 collisionReflection = Vector2.Reflect(rb2d.velocity, collisionNormal);
			Vector2 parallelComponent = Vector2.Dot(collisionNormal, collisionReflection) * collisionNormal;
			Vector2 perpendicularComponent = collisionReflection - parallelComponent;
			Vector2 bounceForce = perpendicularComponent * glanceMultiplier + parallelComponent * bounceMultiplier;
			rb2d.AddForce(bounceForce, ForceMode2D.Impulse);
		}
		else rb2d.velocity = maximum_velocity * rb2d.velocity.normalized;
	}
}
