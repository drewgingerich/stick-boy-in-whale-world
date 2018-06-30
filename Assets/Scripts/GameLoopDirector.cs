using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopDirector : MonoBehaviour {

	public enum MinigameType{ Lung, Heart, LAST }
	[Header("Balance")]
	public float countdownPenalty;

	[Header("Current State")]
	public float countdown;
	public float maxCountdown;
	[SerializeField] List<MinigameType> organsWithProblems;
	[SerializeField] List<MinigameType> organsReady;
	bool problemSolvedFlag = false;
	EventTracker callback;

	[Header("Setup")]
	/// <summary>Use <see cref="MinigameType" /> for the order of scenes</summary>
	public List<EventTracker> allMinigames;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Begin( EventTracker newCallback ) {
		callback = newCallback;
		organsReady.Clear();
		for( int i = 0; i < allMinigames.Count; i++) {
			if( allMinigames[i] != null ){
				organsReady.Add( (MinigameType) i);
			} else {
				organsReady.Add( MinigameType.LAST );
			}
		}
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

	// public void 

	IEnumerator CountdownRoutine() {
		yield return null;
		while( WhaleDirector.inst.gameStage == WhaleDirector.GameStage.GameLoop ) {
			//main game loop
			//start countdown
			for( countdown = maxCountdown; countdown > 0f; countdown -= Time.deltaTime ) {
				yield return null;
			}

			if( !TryCreateNewProblem() ) {
				//YOU LOSE 💀
			}
			
		}
	}

	bool TryCreateNewProblem(MinigameType type = MinigameType.LAST){
		if( organsReady.Count == 0 )
			return false;
		if( type == MinigameType.LAST )
			type = organsReady[ Random.Range(0, organsReady.Count) ];
		organsWithProblems.Add( type );
		organsReady.Remove( type );
		allMinigames[(int) type].StartEvent();
		return true;
	}

	/// <summary>
	/// :̶.̶|̶:̶;̶
	/// </summary>
	void Loss() {
		callback.EventSucceed();
	}
}
