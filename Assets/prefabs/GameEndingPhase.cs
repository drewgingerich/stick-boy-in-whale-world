using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndingPhase : MonoBehaviour {

	public GameObject blockade;
	public float waitToCallOut;
	public EventTracker oldPersonCallout;

	public void StartEnding() {
		//darken the whale
		// open the blockade
		blockade.SetActive(false);
		// start calling out to the player
		StartCoroutine( CallOutToPlayerRoutine() );
			// after x seconds, give a direct command
		
	}

	IEnumerator CallOutToPlayerRoutine() {
		yield return new WaitForSeconds( waitToCallOut );
		oldPersonCallout.StartEvent();
	}
}
