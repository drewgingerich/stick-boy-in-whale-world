using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public static DialogueManager instance;

	[SerializeField] Text dialogueText;
	[SerializeField] Animator animator;

	Queue<string> lines;
	// Coroutine talkRoutine;

	void Awake() {
		if (instance != null)
			Debug.LogError("Multiple WhaleTalk Instances.");
		instance = this;
		lines = new Queue<string>();
	}

	public void Talk(Dialogue dialogue) {
		lines.Clear();
		foreach (string line in dialogue.lines) {
			lines.Enqueue(line);
		}
		DisplayNextLine();
		// if (talkRoutine != null)
		// 	StopCoroutine(talkRoutine);
		// talkRoutine = StartCoroutine(TalkRoutine());
	}

	public void DisplayNextLine() {
		if (lines.Count == 0)
			EndDialogue();
		dialogueText.text = lines.Dequeue();
	}

	void EndDialogue() {

	}
}
