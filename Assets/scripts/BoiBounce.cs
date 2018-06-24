using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoiBounce : MonoBehaviour {

	[Header("What up?")]
	public float glanceMultiplier = 2;
	public float bounceMultiplier = 1.5f;

	Rigidbody2D rb2d;
	Vector2 velocity;
	ContactPoint2D[] contactsBuffer = new ContactPoint2D[4];

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		velocity = rb2d.velocity;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Vector2 collisionNormal = collision.contacts[0].normal;
		Vector2 reflection = Vector2.Reflect(velocity, collisionNormal);
		Vector2 parallel = Vector2.Dot(collisionNormal, reflection) * collisionNormal;
		Vector2 orthogonal = reflection - parallel;
		Vector2 bounce = orthogonal * glanceMultiplier + parallel * bounceMultiplier;
		rb2d.AddForce(bounce, ForceMode2D.Impulse);
	}

	Vector2 GetSumContactNormal() {
		int numContacts = rb2d.GetContacts(contactsBuffer);
		Vector2 sumContactNormal = Vector2.zero;
		for (int i = 0; i < numContacts; i++) {
			sumContactNormal += contactsBuffer[i].normal;
		}
		return sumContactNormal.normalized;
	}
}
