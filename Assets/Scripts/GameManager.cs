using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains global game status information for now
/// </summary>
public class GameManager : MonoBehaviour {

	/// <summary>
	/// Reference me to access the GameManager from anywhere via <code>GameManager.inst</code>
	/// </summary>
	public static GameManager inst = null;

	public GameObject player;

	void OnAwake() {
		if( inst != null ) {
			// Preserve Singleton
			Destroy(gameObject);
		}
		inst = this;
	}

	/// <summary>
	/// Stops player movement for use in minigames or cutscenes
	/// </summary>
	public void DisablePlayerMovement() {
	}

	/// <summary>
	/// Resumes player movement
	/// </summary>
	public void EnablePlayerMovement() {

	}
}
