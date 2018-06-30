﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSequence : MonoBehaviour {

	public UnityEvent OnDialogueFinish;

	public List<Dialogue> dialogues;

	Dialogue selectedDialogue;
	
	public int index = 0;

	public void Play() {
		selectedDialogue = dialogues[index];
		DialoguePlayer.instance.PlayMainDialogue(selectedDialogue);
		selectedDialogue.OnDialogueFinish += FinishDialogue;
		index++;
		if (index == dialogues.Count)
			index = dialogues.Count - 1;
	}

	void FinishDialogue() {
		selectedDialogue.OnDialogueFinish -= FinishDialogue;
		OnDialogueFinish.Invoke();
	}
}
