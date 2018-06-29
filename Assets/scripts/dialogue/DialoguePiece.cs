using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DialoguePiece : ScriptableObject {

	public DialogueCharacter character;
	public List<string> lines;
}
