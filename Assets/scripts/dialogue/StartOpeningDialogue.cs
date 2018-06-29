using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOpeningDialogue : MonoBehaviour {

	[SerializeField] Dialogue startingDialogue;

	void Start () {
		DialoguePlayer.instance.PlayMainDialogue(startingDialogue);
	}
}
