using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StickboiController : MonoBehaviour {

	[SerializeField] Animator anim;
	[SerializeField] SpriteRenderer sprite;
	[SerializeField] float speed = 10;

	Rigidbody2D rb2d;

	[Header("Stick parameters")]
	[SerializeField] float stickRadius = 0.2f;
	[SerializeField] float swingDistance = 1f;
	ContactFilter2D filter;
	RaycastHit2D[] hitBuffer;
	int hitBufferSize = 8;
	bool paused = false;

	public void SetPause(bool value) {
		paused = value;
		if (value)
			rb2d.velocity = Vector2.zero;
	}

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
		int stickLayerIndex = 1 << LayerMask.NameToLayer("Stick");
		filter = new ContactFilter2D();
		filter.useLayerMask = true;
		filter.SetLayerMask(stickLayerIndex);
		hitBuffer = new RaycastHit2D[hitBufferSize];
	}

	void Update () {
		if (paused)
			return;
		Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;		
		rb2d.velocity = (Vector3)inputDirection * speed;
		sprite.flipX = rb2d.velocity.x < 0 ? true : false;
		bool moving = rb2d.velocity.magnitude > 0.1f ? true : false;
		anim.SetBool("moving", moving);
		if (Input.GetMouseButtonDown(0))
			SwingStick();
	}

	void SwingStick() {
		anim.SetTrigger("swing");
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = mousePosition - transform.position;
		direction = direction.normalized;
		int numHits = Physics2D.CircleCast(transform.position, stickRadius, direction, filter, hitBuffer, swingDistance);
		for (int i = 0; i < numHits && i < hitBufferSize; i++) {
			RaycastHit2D hit = hitBuffer[i];
			var interactable = hit.collider.GetComponent<StickInteractable>();
			if (interactable != null) {
				interactable.Hit(hit);
				break;
			}
		}
	}
}
