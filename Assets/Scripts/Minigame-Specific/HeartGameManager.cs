using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HeartGameManager : MonoBehaviour {

	public UnityEvent OnSucceed;

	public enum ChamberDirection { TopLeft, TopRight, BottomLeft, BottomRight, LAST };
	// [SerializeField] Heart heart;

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
	bool demoDoneFlag = false;

	[Header("UI Setup")]
	[SerializeField] Animator heartAnimator;
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
		// Debug.Log( "Chamber " + thisDirection + " was poked" );
		buttonsPressedThisFrame.Add( (ChamberDirection) thisDirection );
		if( minigameRunning) {
			heartAnimator.SetInteger("highlightChamber", (int)thisDirection);
			heartAnimator.SetTrigger("doHighlightChamber");
		}

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
		heartAnimator.SetBool("heartStopped", true);
		// Generate new pattern item
		currentPattern.Add( (ChamberDirection) Random.Range( 0, (int)ChamberDirection.LAST ) );

		// Display pattern
		yield return StartCoroutine( DemoPatternRoutine( currentPattern ));


		bool listening = true;
		bool entering = false;
		int currentStepInPattern = 0;

		// attract mode
		while( buttonsPressedThisFrame.Count == 0 ) {
			yield return new WaitForSeconds( timeBetweenChamberShake * 3 );
			Coroutine currentDemo = StartCoroutine( DemoPatternRoutine( currentPattern ));
			while ( !demoDoneFlag ) {
				if( buttonsPressedThisFrame.Count > 0 ) {
					StopCoroutine( currentDemo );
					break;
				}
				yield return null;
			}
			if( buttonsPressedThisFrame.Count > 0 ) {
				StopCoroutine( currentDemo );
				entering = true;
				break;
			}

		}
		while( entering ) {
			// entering = true;
			if( buttonsPressedThisFrame.Count > 0 ) {
				if( buttonsPressedThisFrame[0] == currentPattern[currentStepInPattern] ){
					currentStepInPattern++;
					Debug.Log("Correct button of " + buttonsPressedThisFrame[0]);
				} else {
					Debug.Log("You failed! You pressed " + buttonsPressedThisFrame[0] + " instead of " + currentPattern[currentStepInPattern]);
					// heart.TakeDamage(10f);
					// EndOfTurnCleanup();
					// callback.EventFail();
					StartCoroutine( TurnRoutine() );
					// yield break;
				}
				if( currentStepInPattern >= currentPattern.Count ) {
					// Victory, end turn
					Debug.Log("You won!");
					OnSucceed.Invoke();
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
		demoDoneFlag = false;
		int idleState = Animator.StringToHash("Stopped");
		foreach( ChamberDirection thisDirection in thisPattern ) {
			// change color of chamber?
			// start chamber shaking
			// yield return StartCoroutine( ShakeChamber( chambers[(int)thisDirection] ));
			// chambers[(int)thisDirection].Shake( .5f );
			// short time between chamber shakes
			heartAnimator.SetInteger("highlightChamber", (int)thisDirection);
			heartAnimator.SetTrigger("doHighlightChamber");
			// while( heartAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != idleState ) {
			// 	Debug.Log("wait");
			// 	yield return null;
			// }
			yield return new WaitForSeconds( timeBetweenChamberShake );
		}
		demoDoneFlag = true;
	}

	void EndOfTurnCleanup() {
		foreach( HeartChamber thisChamber in chambers ) {
			// thisChamber.interactable = false;
		}
		heartAnimator.SetBool("heartStopped", false);
		minigameRunning = false;
	}
}
