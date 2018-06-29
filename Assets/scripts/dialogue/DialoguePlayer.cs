using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePlayer : MonoBehaviour {

	[SerializeField] GameObject dialogueUI;
	[SerializeField] GameObject dialogueVeil;
	[SerializeField] Image characterSprite;
	[SerializeField] Text nameText;
	[SerializeField] Text lineText;
	[SerializeField] float lingerTime = 2f;

	public static DialoguePlayer instance;

	Dialogue dialogue;
	DialoguePiece currentPiece;
	string currentLine;
	int pieceIndex;
	int lineIndex;
	Coroutine playLineRoutine;
	Coroutine lingerRoutine;
	bool lingering;
	bool playing;

	void Awake() {
		Debug.Assert(instance == null);
		instance = this;
		dialogueVeil.SetActive(false);
		dialogueUI.SetActive(false);
	}

	void Update() {
		if (Input.GetMouseButtonDown(0))
			Interrupt();
	}

	public void Interrupt() {
		if (lingering) {
			StopCoroutine(lingerRoutine);
			PlayNextLine();
		} else if (playing) {
			StopCoroutine(playLineRoutine);
			lineText.text = currentLine;
			lingerRoutine = StartCoroutine(LingerRoutine());
		}
	}

	public void PlayDialogue(Dialogue dialogue) {
		dialogueUI.SetActive(true);
		this.dialogue = dialogue;
		pieceIndex = 0;
		lineIndex = 0;
		PlayNextPiece();
	}

	public void PlayMainDialogue(Dialogue dialogue) {
		dialogueVeil.SetActive(true);
		PlayDialogue(dialogue);
	}

	void PlayNextPiece() {
		if (pieceIndex == dialogue.pieces.Count) {
			FinishDialogue();
			return;
		}
		currentPiece = dialogue.pieces[pieceIndex];
		characterSprite.sprite = currentPiece.character.sprite;
		nameText.text = currentPiece.character.name;
		PlayNextLine();
		pieceIndex++;
	}

	void PlayNextLine() {
		if (lineIndex == currentPiece.lines.Count) {
			lineIndex = 0;
			PlayNextPiece();
			return;
		}
		if (playLineRoutine != null)
			StopCoroutine(playLineRoutine);
		currentLine = currentPiece.lines[lineIndex];
		playLineRoutine = StartCoroutine(PlayNextLineRoutine(currentLine));
		lineIndex++;
	}

	IEnumerator PlayNextLineRoutine(string line) {
		playing = true;
		lineText.text = "";
		foreach(char c in line.ToCharArray()) {
			lineText.text += c;
			yield return new WaitForSeconds(0.03f);
		}
		playing = false;
		lingerRoutine = StartCoroutine(LingerRoutine());
	}

	IEnumerator LingerRoutine() {
		lingering = true;
		yield return new WaitForSeconds(lingerTime);
		lingering = false;
		PlayNextLine();
	}

	void FinishDialogue() {
		dialogueVeil.SetActive(false);
		dialogueUI.SetActive(false);
	}
}
