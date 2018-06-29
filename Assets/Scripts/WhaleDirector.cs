using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Directs the whole dang whale game
/// </summary>
public class WhaleDirector : MonoBehaviour {
	public enum Minigame{ Lungs, Heart, Stomach };
	public enum PlayerState{ Playing, Stopped };
	public static WhaleDirector inst;
	[Header("Current State")]
	public PlayerState playerState;
	[SerializeField] List<Minigame> currentChallenges;
	[SerializeField] float currentDifficultyModifier;
	[Header("Setup")]
	public Transform player;

	void Awake() {
		inst = this;
	}

	public void StartChallenges() {

	}

	public void PauseChallenges() {
	}

	public void SetPlayerState(PlayerState newState ) {
		if( newState == PlayerState.Playing ) {
			player.GetComponent<GolfMovement>().enabled = true;
			player.GetComponent<PlayerSwingStick>().enabled = true;
			playerState = newState;
		} else if( newState == PlayerState.Stopped ) {
			player.GetComponent<GolfMovement>().enabled = false;
			player.GetComponent<PlayerSwingStick>().enabled = false;
			playerState = newState;
		}
		
	}

	public void ResumeChallenges() {

	}

	/// <summary>
	/// Handles the flow of the game during challenges.
	/// </summary>
	/// <returns></returns>
	IEnumerator ChallengeRoutine() {

	}
}
