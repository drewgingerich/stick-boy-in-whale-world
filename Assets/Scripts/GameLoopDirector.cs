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
	bool problemSolvedFlag = false;
	EventTracker loopStartCallback;
	Coroutine currentCountdownRoutine;

	[Header("Setup")]
	// public EventTracker eventTracker;
	/// <summary>Use <see cref="MinigameType" /> for the order of scenes</summary>
	public List<EventTracker> allMinigames;
	[Header("Debug UI")]
	[SerializeField] Slider countdownSlider;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( countdownSlider != null) {
			countdownSlider.value = countdown;
			countdownSlider.maxValue = maxCountdown;
		}
	}

	public void Begin( EventTracker newCallback ) {
		WhaleDirector.inst.SetPlayerState( WhaleDirector.PlayerState.Playing );
		loopStartCallback = newCallback;
		organsReady.Clear();
		for( int i = 0; i < allMinigames.Count; i++) {
			if( allMinigames[i] != null ){
				organsReady.Add( (MinigameType) i);
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
		organsWithProblems.Add( type );
		organsReady.Remove( type );
		allMinigames[(int) type].StartEvent();
		Debug.Log("Created at problem in the " + type );
		return true;
	}

	/// <summary>
	/// :̶.̶|̶:̶;̶ haha its loss.jpg
	/// </summary>
	void Loss() {
		Debug.Log("Player has lost");
		StopAllCoroutines();
		loopStartCallback.EventFail();
	}
}
