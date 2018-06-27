using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickInteractable : MonoBehaviour {

	public UnityEvent OnPokedByStick;
	// public bool requireSpecificStickInteraction = false;
	// public PlayerSwingStick.StickInteractionType desiredStickInteraction = PlayerSwingStick.StickInteractionType.Poke;

	void OnCollisionEnter2D(Collision2D collision) {
		if( collision.gameObject.CompareTag("Stick") ) {
			// PlayerSwingStick.StickInteractionType thisStickInteraction = collision.transform.GetComponentInParent<PlayerSwingStick>().currentStickInteraction;
			// if( thisStickInteraction == desiredStickInteraction ){
			OnPokedByStick.Invoke();
			SendMessage("Poked");
			// } else {
			// 	Debug.Log( "Player used wrong stick interaction type of " + thisStickInteraction + " instead of  " + desiredStickInteraction + " on " + gameObject);
			// }
			
		}
	}

}
