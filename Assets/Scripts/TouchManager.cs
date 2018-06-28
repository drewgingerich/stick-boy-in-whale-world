using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This manager works in concert with <see cref="FingerObj" /> to handle multitouch fingers. The manager only handles the lifecycle for fingers.
/// 
/// **Be sure to check out the events:**
/// - <see cref="TouchManager.OnNewFinger"/>
/// 
/// - <see cref="TouchManager.FingersDoneUpdating"/>
/// 
/// - <see cref="TouchManager.OnFingerUp"/>
/// </summary>
public class TouchManager : MonoBehaviour {

	public static TouchManager inst;
	public delegate void FingerEvent( FingerObj affectedFinger );
	public delegate void ManagerEvent();
	/// <summary>
	/// Once a new finger is being tracked. Delegate event must accept `FingerObj` as the argument
	/// </summary>
	public event FingerEvent OnNewFinger;
	/// <summary>
	/// Once all <see cref="FingerObj.UpdateFinger"/> functions have been called
	/// </summary>
	public event ManagerEvent FingersDoneUpdating;
	/// <summary>
	/// The moment _right before_ a finger is about to be destroyed. Delegate event must accept `FingerObj` as the argument
	/// </summary>
	// public event FingerEvent OnFingerUp;
	// public event FingerEvent OnTapComplete;
	// public event FingerEvent OnDragComplete;

	[Header("Current State")]
	[SerializeField] List<FingerObj> currentFingers = new List<FingerObj>();

	[Header("Balance")]
	public float dragDetectionDistPixels;

	[Header("Setup")]
	public FingerObj fingerPrefab;

	void Awake() {
		if( inst != null)
			Destroy( inst );
		inst = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach( Touch thisTouch in Input.touches ) {
			if( thisTouch.phase == TouchPhase.Began ) {
				// we must have a new baby finger!
				FingerObj newFinger = Instantiate( fingerPrefab, TouchHelper.GetTouchWorldPosition( thisTouch ), Quaternion.identity );
				newFinger.fingerID = thisTouch.fingerId;
				newFinger.transform.position = TouchHelper.GetTouchWorldPosition( thisTouch );
				newFinger.touchLastFrame = newFinger.touch = newFinger.originTouch = thisTouch;
				currentFingers.Add( newFinger );
				OnNewFinger( newFinger );	
			}
		}
		foreach( FingerObj thisFinger in currentFingers ) {
			thisFinger.UpdateFinger();
		}
		// FingersDoneUpdating();
	}

	/// <summary>
	/// **WARNING:** This is only to be called by <see cref="FingerObj.OnDestroy" />.
	/// 
	/// _Story time: It turns out C# does not have anything similar to the concept of a C++ `friend` so this warning will have to do._
	/// </summary>
	public void FingerDestroyed( int destroyedID ) {
		for( int i = 0; i < currentFingers.Count; i++ ) {
			if( currentFingers[i].fingerID == destroyedID ) {
				// OnFingerUp(currentFingers[i]);
				currentFingers.RemoveAt(i);
				return;
			}
		}
		Debug.LogError("Finger of ID " + destroyedID + " was not found in list during cleanup time.");
	}

	public FingerObj GetNewestFinger() {
		return currentFingers[ currentFingers.Count - 1 ];
	}

	
}