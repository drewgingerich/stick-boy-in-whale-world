﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GolfMovement : MonoBehaviour {

	enum InputMode { Idle, ReceivingInput, LAST };
	
	[Header("Balance")]
	public float movementForceModifier = .1f;
	public bool useTapToMove = false;
	
	[Header("Current State")]
	[SerializeField] InputMode currentMode;
	LineRenderer lineRenderer;
	ShakeOnCommand shakeOnCommand;
	Rigidbody2D rbod;
	[SerializeField] FingerObj fingerOfInterest;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		lineRenderer = GetComponent<LineRenderer>();
		rbod = GetComponent<Rigidbody2D>();
		shakeOnCommand = GetComponent<ShakeOnCommand>();
		// Debug.Log( TouchManager.inst );
		if( TouchManager.inst != null )
			TouchManager.inst.OnNewFinger += OnNewFinger;
	}

	void Start() {
		TouchManager.inst.OnNewFinger += OnNewFinger;
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDisable() {
		TouchManager.inst.OnNewFinger -= OnNewFinger;
	}

	void OnNewFinger(FingerObj newFinger) {
		if( TouchHelper.IsObjectUnderTouch(newFinger.originTouch, transform, .5f) ) {
			fingerOfInterest = newFinger;
			fingerOfInterest.OnStatusChange += FingerStatusChange;
		}
	}

	/// <summary>
	/// Check out <see cref="TouchManager.FingersDoneUpdating"/>. Only calls when the status changes, not every frame.
	/// </summary>
	void FingerStatusChange( FingerObj foi ) {
		Debug.Log( foi.name + " state's changed to " + foi.currentState );
		if( fingerOfInterest.currentState == FingerObj.TouchState.Ending ) {
			if( currentMode == InputMode.ReceivingInput ) {
				Shoot();
				fingerOfInterest = null;
				currentMode = InputMode.Idle;
			} else {
				fingerOfInterest = null;
			}
		} else if ( fingerOfInterest.currentState == FingerObj.TouchState.Dragging ) {
			StartCoroutine( GolfSwingRoutine() );
		}
	}

	IEnumerator GolfSwingRoutine() {
		// Debug.Log("Touch Down");
		lineRenderer.enabled = true;
		currentMode = InputMode.ReceivingInput;
		Vector3 touchPos = Vector3.zero;
		while( currentMode == InputMode.ReceivingInput) {
			// Debug.Log(Input.touches[0].phase);
			Vector3 playerPosition = transform.position;
			playerPosition.z -= 2f;
			lineRenderer.SetPosition(0, playerPosition );
			// Debug.Log( TouchHelper.WhatIsUnderFinger( Input.touches[0] ) );
			touchPos = fingerOfInterest.transform.position;
			touchPos.z = transform.position.z - 2;
			// TouchHelper.RotateToFace2D(rbod, touchPos );
			lineRenderer.SetPosition(1, touchPos );
			if( shakeOnCommand != null )
				shakeOnCommand.ShakeOnce();
			yield return null;
		}
		yield return null;
		// Debug.Log("Touch Up");
		currentMode = InputMode.Idle;
		lineRenderer.enabled = false;
		if( shakeOnCommand != null )
			shakeOnCommand.Reset();
	}

	void Shoot() {
		// because we're already facing `+X` we can just shoot ourselves in that direction
		Vector2 force = (Vector2)fingerOfInterest.transform.position - (Vector2)transform.position; 
		rbod.AddForce( force * movementForceModifier, ForceMode2D.Impulse );
	}
}
