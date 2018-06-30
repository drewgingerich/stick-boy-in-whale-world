using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequence : MonoBehaviour {

	public List<Dialogue> dialogues;
	
	public int index = 0;

	public void Play() {
		DialoguePlayer.instance.PlayDialogue(dialogues[index]);
		index++;
		if (index == dialogues.Count)
			index = dialogues.Count - 1;
	}
}
