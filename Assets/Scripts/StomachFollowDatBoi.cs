using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachFollowDatBoi : MonoBehaviour {

	[Header("It dat boi!!")]
	[SerializeField] Transform datBoi;
	[SerializeField] float lagTime = 0.25f;

	void Update () {
		Vector2 diff = new Vector2(datBoi.position.x - transform.position.x, 0.0f);
		Vector2 move = diff / lagTime * Time.deltaTime;
		transform.position += (Vector3)move;
	}
}
