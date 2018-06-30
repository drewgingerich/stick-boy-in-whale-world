using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Directs the whole dang whale game.null
/// 
/// See https://melonballbouncegroup.slack.com/files/UBCH9JTPW/FBGE5H7PE/Game_Flow
/// </summary>
public class WhaleDirector : MonoBehaviour {
	public enum PlayerState{ Playing, Paused };
	public enum GameStage{ MainMenu, Introduction, Tutorial, PreGame, GameLoop, GameOver, Exit, LAST };
	public static WhaleDirector inst;
	[Header("Current State")]
	public PlayerState playerState;
	public GameStage gameStage;
	[Header("Setup")]
	public Transform player;
	// [SerializeField] EventTracker mainMenuScene;
	// [SerializeField] EventTracker introductionScene;
	// [SerializeField] EventTracker tutorialScene;
	// [SerializeField] EventTracker preGameScene;
	// [SerializeField] EventTracker gameLoopScene;
	// [SerializeField] EventTracker gameOverScene;
	// [SerializeField] EventTracker exitScene;
	/// <summary>Use <see cref="GameStage" /> for the order of scenes</summary>
	[Tooltip("Look at the WhaleDirector.GameStage enum for the order of scenes")] public List<EventTracker> sceneList = new List<EventTracker>();
	// [SerializeField] EventTracker optionalTransitionEvent;

	void Awake() {
		inst = this;
	}

	void Start() {
	}

	public void SetPlayerState(PlayerState newState ) {
		if( newState == PlayerState.Playing ) {
			player.GetComponent<GolfMovement>().enabled = true;
			player.GetComponent<PlayerSwingStick>().enabled = true;
			playerState = newState;
		} else if( newState == PlayerState.Paused ) {
			player.GetComponent<GolfMovement>().enabled = false;
			player.GetComponent<PlayerSwingStick>().enabled = false;
			playerState = newState;
		}
		
	}

	public void AdvanceStage() {
		gameStage++;
		if( gameStage == GameStage.LAST )
			gameStage = GameStage.MainMenu;
		sceneList[ (int)gameStage ].StartEvent();
	}

	public void ForceSetStage( GameStage newStage ) {
		gameStage = newStage;
		sceneList[ (int)gameStage ].StartEvent();
	}

	IEnumerator GameLoop() {
		yield return null;
	}
	
}
