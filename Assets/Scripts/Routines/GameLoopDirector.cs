using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoopDirector : MonoBehaviour {

	public enum MinigameType{ Lung, Heart, LungDebris, IntestineDebris, LAST };
	[Header("Current State")]
	public float countdown;
	public float maxCountdown;
	public float countdownPenalty;
	List<MinigameType> organsWithProblems  = new List<MinigameType>();
	List<MinigameType> organsReady = new List<MinigameType>();
	List<bool> tutorialDone = new List<bool>();
	bool problemSolvedFlag = false;
	EventTracker loopStartCallback;
	Coroutine currentCountdownRoutine;
	bool pauseCountdownFlag = false;

	[Header("Setup")]
	// public EventTracker eventTracker;
	/// <summary>Use <see cref="MinigameType" /> for the order of scenes</summary>
	public List<EventTracker> allMinigames;
	public List<EventTracker> allTutorials;
	[Header("Debug UI")]
	[SerializeField] Slider countdownSlider;


	// Use this for initialization
	void Start () {
		if( countdownSlider != null) {
			countdownSlider.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( countdownSlider != null) {
			countdownSlider.maxValue = maxCountdown;
			countdownSlider.value = countdown;
		}
	}

	public void Begin( EventTracker newCallback ) {
		if( countdownSlider != null) {
			countdownSlider.gameObject.SetActive(true);
		}
		WhaleDirector.inst.SetPlayerState( WhaleDirector.PlayerState.Playing );
		loopStartCallback = newCallback;
		organsReady.Clear();
		for( int i = 0; i < allMinigames.Count; i++) {
			if( allMinigames[i] != null ){
				organsReady.Add( (MinigameType) i);
				tutorialDone.Add( false );
			} else {
				organsReady.Add( MinigameType.LAST );
			}
		}
		currentCountdownRoutine = StartCoroutine(CountdownRoutine());
	}


	public void MinigameFailedButAlive(EventTracker callback) {
		// who heck'd up
		MinigameType offender = MinigameType.LAST;
		for( int i = 0; i < allMinigames.Count; i++ ) {
			if( allMinigames[i] == callback ) {
				offender = (MinigameType) i;
			}
		}
		if( offender == MinigameType.LAST )
			Debug.LogError("Couldn't find minigame associated with " + callback);
		organsWithProblems.Remove( offender );
		organsReady.Add( offender );
		// PUNISH PUNISH PUNISH
		maxCountdown -= countdownPenalty;
	}

	public void MinigameWon( EventTracker callback ) {
		MinigameType offender = MinigameType.LAST;
		for( int i = 0; i < allMinigames.Count; i++ ) {
			if( allMinigames[i] == callback ) {
				offender = (MinigameType) i;
			}
		}
		if( offender == MinigameType.LAST )
			Debug.LogError("Couldn't find minigame associated with " + callback);
		organsWithProblems.Remove( offender );
		organsReady.Add( offender );
		StopCoroutine( currentCountdownRoutine );
		currentCountdownRoutine = StartCoroutine( CountdownRoutine() );
	}

	IEnumerator CountdownRoutine() {
		// yield return null;
		while( WhaleDirector.inst.gameStage == WhaleDirector.GameStage.GameLoop ) {
			//main game loop
			// This will create the new problem
			if( !TryCreateNewProblem() ) {
				//YOU LOSE 💀
				Loss(); // this will break out of the coroutine
			}
			//start countdown
			for( countdown = maxCountdown; countdown > 0f; countdown -= Time.deltaTime ) {
				while( pauseCountdownFlag ) {
					yield return null;
				}
				yield return null;
			}
		}
	}

	bool TryCreateNewProblem(MinigameType type = MinigameType.LAST){
		Debug.Log("Trying to create new problem");
		if( organsReady.Count == 0 )
			return false;
		if( type == MinigameType.LAST ) 
			type = organsReady[ Random.Range(0, organsReady.Count) ];
		Debug.Log("New problem will occur in the " + type);
		// if( !tutorialDone[(int) type] ) {
		// 	if( allTutorials[(int) type] != null ) {
				allTutorials[(int) type].StartEvent();
				PauseCountdown();
		// 	} else {
		// 		Debug.LogWarning("No tutorial found for " + type );
		// 	}
		// 	tutorialDone[(int) type] = true;
		// }
		// organsWithProblems.Add( type );
		// organsReady.Remove( type );
		allMinigames[(int) type].StartEvent();
		Debug.Log("Created at problem in the " + type );
		return true;
	}

	/// <summary>
	/// :̶.̶|̶:̶;̶ haha its loss.jpg
	/// </summary>
	void Loss() {
		Debug.Log("Player has lost");
		if( countdownSlider != null) {
			countdownSlider.gameObject.SetActive(false);
		}
		StopAllCoroutines();
		loopStartCallback.EventFail();
	}

	public void PauseCountdown() {
		pauseCountdownFlag = true;
		WhaleDirector.inst.SetPlayerState( WhaleDirector.PlayerState.Paused );
	}

	public void ResumeCountdown() {
		pauseCountdownFlag = false;
		WhaleDirector.inst.SetPlayerState( WhaleDirector.PlayerState.Playing );
	}

}
