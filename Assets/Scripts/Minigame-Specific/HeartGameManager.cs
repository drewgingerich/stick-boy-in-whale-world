using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartGameManager : MonoBehaviour {
	public enum ChamberDirection { TopLeft, TopRight, BottomLeft, BottomRight, LAST };
	[SerializeField] Heart heart;

	[Header("Balance")]
	[SerializeField] float timeBetweenChamberShake = .25f;
	[SerializeField] float durationChamberShake = 1f;
	[SerializeField] float shakeAmount = .5f;
	// [SerializeField] float maxTimeToPressButtons = 4f;


	[Header("Game State")]
	/// <summary> The current simon-says pattern. Pattern is additive. </summary>
	[SerializeField] List<ChamberDirection> currentPattern;
	[SerializeField] List<ChamberDirection> buttonsPressedThisFrame;
	[SerializeField] bool minigameRunning = false;

	[Header("UI Setup")]
	[SerializeField] HeartChamber tlChamber;
	[SerializeField] HeartChamber trChamber;
	[SerializeField] HeartChamber blChamber;
	[SerializeField] HeartChamber brChamber;

	/// <summary> All four buttons in a list </summary>
	List<HeartChamber> chambers = new List<HeartChamber>();

	void Awake() {
		chambers.Add( tlChamber );
		chambers.Add( trChamber );
		chambers.Add( blChamber );
		chambers.Add( brChamber );
		// for( int i = 0; i < chambers.Count; i++ ) {
		// 	// Debug.Log(i);
		// 	// See https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html
		// 	// chamberButtons[i].onClick.AddListener( (delegate {
		// 	// 	ButtonPressed( (chamberDirection) i ); 
		// 	// } ) );
		// 	chambers[i].interactable = false;
		// }
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
		buttonsPressedThisFrame.Clear();
	}

	/// <param name="thisDirection">Utilizes the order found in <see cref="ChamberDirection" /> </param>
	public void ChamberHit( int thisDirection ){
		Debug.Log( thisDirection );
		buttonsPressedThisFrame.Add( (ChamberDirection) thisDirection );
		// TODO: also animate the button being pressed?
	}

	public void StartMinigame() {
		if( !minigameRunning ) {
			StartCoroutine( TurnRoutine() );
		}
	}

	void ResetCurrentPattern() {
		currentPattern.Clear();
		currentPattern.Add( (ChamberDirection) Random.Range( 0, (int) ChamberDirection.LAST ) );
	}

	/// <summary>
	/// A single turn's game routine
	/// </summary>
	IEnumerator TurnRoutine() {
		minigameRunning = true;
		// Generate new pattern item
		currentPattern.Add( (ChamberDirection) Random.Range( 0, (int)ChamberDirection.LAST ) );

		// Display pattern
		yield return StartCoroutine( DemoPatternRoutine( currentPattern ));

		// Get user inputs
		foreach( HeartChamber thisChamber in chambers ) {
			// thisChamber.interactable = true;
		}
		bool listening = true;
		int currentStepInPattern = 0;
		while( listening ) {
			if( buttonsPressedThisFrame.Count > 0 ) {
				if( buttonsPressedThisFrame[0] == currentPattern[currentStepInPattern] ){
					currentStepInPattern++;
					Debug.Log("Correct button of " + buttonsPressedThisFrame[0]);
				} else {
					Debug.Log("You failed! You pressed " + buttonsPressedThisFrame[0] + " instead of " + currentPattern[currentStepInPattern]);
					// TODO: Add penalty to loosing.
					heart.TakeDamage(10f);
					EndOfTurnCleanup();
					yield break;
				}
				if( currentStepInPattern >= currentPattern.Count ) {
					// Victory, end turn
					Debug.Log("You won!");
					EndOfTurnCleanup();
					yield break;
				}
			}
			yield return null;
		}
	}

	/// <summary>
	/// Demonstrates the chamber pattern. User cannot click until the routine is done
	/// </summary>
	IEnumerator DemoPatternRoutine( List<ChamberDirection> thisPattern ) {
		foreach( ChamberDirection thisDirection in thisPattern ) {
			// change color of chamber?
			// start chamber shaking
			// yield return StartCoroutine( ShakeChamber( chambers[(int)thisDirection] ));
			chambers[(int)thisDirection].Shake( .5f );
			// short time between chamber shakes
			yield return new WaitForSeconds( timeBetweenChamberShake );
		}
	}

	void EndOfTurnCleanup() {
		foreach( HeartChamber thisChamber in chambers ) {
			// thisChamber.interactable = false;
		}
		minigameRunning = false;
	}
}
