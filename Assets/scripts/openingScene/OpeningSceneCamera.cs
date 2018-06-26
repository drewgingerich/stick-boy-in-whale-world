﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSceneCamera : MonoBehaviour {

	Coroutine moveRoutine;

	public void Move(Vector2 move) {
		if (moveRoutine != null) {
			StopCoroutine(moveRoutine);
			transform.position = new Vector3(0, 0, transform.position.z);
		}
		moveRoutine = StartCoroutine(MoveRoutine(move));
	}

	IEnumerator MoveRoutine(Vector2 move) {
		while (true) {
			transform.position += (Vector3)move * Time.deltaTime;
			yield return null;
		}
	}
}
