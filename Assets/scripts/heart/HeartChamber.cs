using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(StickInteractable))]
public class HeartChamber : MonoBehaviour {

	public enum Position { TopRight, BottomRight, BottomLeft, TopLeft }

	public event System.Action<HeartChamber> OnHit = delegate { };

	public Position position;

	public void Hit() {
		OnHit(this);
	}
}
