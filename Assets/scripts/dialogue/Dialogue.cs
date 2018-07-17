using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Dialogue : ScriptableObject {

	public event System.Action OnDialogueFinish = delegate { };
	public List<DialoguePiece> pieces;
	public bool isCutscene;

	public void Finish() {
		OnDialogueFinish();
	}
}
