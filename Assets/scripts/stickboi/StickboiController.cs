using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StickboiController : MonoBehaviour {

	[SerializeField] float speed = 10;

	Rigidbody2D rb2d;

	[Header("Stick parameters")]
	[SerializeField] float stickRadius = 0.2f;
	[SerializeField] float swingDistance = 0.5f;
	ContactFilter2D filter;
	RaycastHit2D[] hitBuffer;
	int hitBufferSize = 8;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
		int stickLayerIndex = LayerMask.NameToLayer("Stick");
		filter = new ContactFilter2D();
		filter.SetLayerMask(stickLayerIndex);
		hitBuffer = new RaycastHit2D[hitBufferSize];
	}

	void Update () {
		Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;		
		rb2d.velocity = (Vector3)inputDirection * speed;
		if (Input.GetMouseButtonDown(0))
			SwingStick();
	}

	void SwingStick() {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = (mousePosition - transform.position).normalized;
		int numHits = Physics2D.CircleCast(transform.position, stickRadius, direction, filter, hitBuffer, swingDistance);
		for (int i = 0; i < numHits && i < hitBufferSize; i++) {
			RaycastHit2D hit = hitBuffer[i];
			var interactable = hit.collider.GetComponent<StickInteractable>();
			if (interactable != null) {
				interactable.Hit(hit);
			}
		}
	}
}
