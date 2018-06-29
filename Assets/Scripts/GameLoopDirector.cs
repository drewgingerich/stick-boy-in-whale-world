using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopDirector : MonoBehaviour {

	public enum Minigames{ Lung, Heart, LAST }
	[Header("Balance")]
	public float countdownPenalty;

	[Header("Current State")]
	public float countdown;
	public float maxCountdown;
	[SerializeField] List<Minigames> organsWithProblems;
	[SerializeField] List<Minigames> organsAlive;

	[Header("Setup")]
	/// <summary>Use <see cref="Minigames" /> for the order of scenes</summary>
	public List<EventTracker> minigames;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Begin() {
		organsAlive.Clear();
		for( int i = 0; i < minigames.Count; i++) {
			if( minigames[i] != null ){
				organsAlive.Add( (Minigames) i);
			} else {
				organsAlive.Add( Minigames.LAST );
			}
		}
	}

	public void MinigameFailed() {
		// PUNISH PUNISH PUNISH
		maxCountdown -= countdownPenalty;
	}

	IEnumerator CountdownRoutine() {
		yield return null;
		while( WhaleDirector.inst.gameStage == WhaleDirector.GameStage.GameLoop ) {
			
		}
	}
}
