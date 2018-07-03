using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WhaleEvent : MonoBehaviour {

	public UnityEvent OnStart;
	public UnityEvent OnSucceed;
	public UnityEvent OnFail;

	public void StartEvent() {
		OnStart.Invoke();
	}

	public void Succeed() {
		OnSucceed.Invoke();
	}

	public void Fail() {
		OnFail.Invoke();
	}
}
