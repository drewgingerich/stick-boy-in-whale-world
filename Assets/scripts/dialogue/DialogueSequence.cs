using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DialogueSequence : MonoBehaviour {

	public UnityEvent OnDialogueFinish;

	[FormerlySerializedAs("dialogues")]
	public List<Dialogue> dialogueSequence;
	public List<Dialogue> dialoguePool;

	Dialogue selectedDialogue;
	
	int sequenceIndex = 0;

	public void Play() {
		if (sequenceIndex == dialogueSequence.Count) {
			selectedDialogue = GetDialogueFromPool();
			if (selectedDialogue == null) {
				OnDialogueFinish.Invoke();
				return;
			}
		} else {
			selectedDialogue = dialogueSequence[sequenceIndex];
			sequenceIndex++;
		}
		DialoguePlayer.instance.PlayDialogue(selectedDialogue);
		selectedDialogue.OnDialogueFinish += FinishDialogue;
	}

	Dialogue GetDialogueFromPool() {
		if (dialoguePool.Count == 0)
			return null;
		else
			return dialoguePool[Random.Range(0, dialoguePool.Count)];
	}

	void FinishDialogue() {
		selectedDialogue.OnDialogueFinish -= FinishDialogue;
		OnDialogueFinish.Invoke();
	}
}
