using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartGameManager : MonoBehaviour {
	public enum chamberDirection { TopLeft, TopRight, BottomLeft, BottomRight, LAST };
	[SerializeField] Heart heart;

	[Header("Balance")]
	[SerializeField] float timeBetweenChamberShake = .25f;
	[SerializeField] float durationChamberShake = 1f;
	[SerializeField] float shakeAmount = .5f;
	// [SerializeField] float maxTimeToPressButtons = 4f;


	[Header("Game State")]
	/// <summary> The current simon-says pattern. Pattern is additive. </summary>
	[SerializeField] List<chamberDirection> currentPattern;
	[SerializeField] List<chamberDirection> buttonsPressedThisFrame;
	bool minigameRunning = false;

	[Header("UISetup")]
	[SerializeField] Button tlButton;
	[SerializeField] Button trButton;
	[SerializeField] Button blButton;
	[SerializeField] Button brButton;
	[SerializeField] ColorBlock highlightedButtonColors;

	/// <summary> All four buttons in a list </summary>
	[SerializeField] List<Button> chamberButtons = new List<Button>();

	void Awake() {
		chamberButtons.Add( tlButton );
		chamberButtons.Add( trButton );
		chamberButtons.Add( blButton );
		chamberButtons.Add( brButton );
		for( int i = 0; i < chamberButtons.Count; i++ ) {
			// Debug.Log(i);
			// See https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html
			// chamberButtons[i].onClick.AddListener( (delegate {
			// 	ButtonPressed( (chamberDirection) i ); 
			// } ) );
			chamberButtons[i].interactable = false;
		}
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
		buttonsPressedThisFrame.Clear();
	}

	public void ButtonPressed( int thisDirection ){
		Debug.Log( thisDirection );
		buttonsPressedThisFrame.Add( (chamberDirection) thisDirection );
		// TODO: also animate the button being pressed?
	}

	public void StartMinigame() {
		if( !minigameRunning ) {
			StartCoroutine( TurnRoutine() );
		}
	}

	void ResetCurrentPattern() {
		currentPattern.Clear();
		currentPattern.Add( (chamberDirection) Random.Range( 0, (int) chamberDirection.LAST ) );
	}

	/// <summary>
	/// A single turn's game routine
	/// </summary>
	IEnumerator TurnRoutine() {
		minigameRunning = true;
		// Generate new pattern item
		currentPattern.Add( (chamberDirection) Random.Range( 0, (int)chamberDirection.LAST ) );

		// Display pattern
		yield return StartCoroutine( DemoPatternRoutine( currentPattern ));

		// Get user inputs
		foreach( Button thisButton in chamberButtons ) {
			thisButton.interactable = true;
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
	IEnumerator DemoPatternRoutine( List<chamberDirection> thisPattern ) {
		foreach( chamberDirection thisDirection in thisPattern ) {
			// change color of chamber?
			// start camera shaking
			yield return StartCoroutine( ShakeChamber( chamberButtons[(int)thisDirection] ));
			// short time between chamber shakes
			yield return new WaitForSeconds( timeBetweenChamberShake );
		}
	}
	
	IEnumerator ShakeChamber( Button thisButton ) {
		Quaternion originalRotation = thisButton.transform.rotation;
		ColorBlock originalColors = thisButton.colors;
		thisButton.colors = highlightedButtonColors;
		for( float timeLeft = durationChamberShake; timeLeft > 0f; timeLeft -= Time.deltaTime ) {
			// Stolen from http://wiki.unity3d.com/index.php/Camera_Shake
			Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
			rotationAmount.x = rotationAmount.y = 0; // Only rotate on Z axis
			thisButton.gameObject.transform.localRotation = Quaternion.Euler (rotationAmount);
			yield return null;
		}
		thisButton.transform.rotation = originalRotation;
		thisButton.colors = originalColors;
	}

	void EndOfTurnCleanup() {
		foreach( Button thisButton in chamberButtons ) {
			thisButton.interactable = false;
		}
		minigameRunning = false;
	}
}
