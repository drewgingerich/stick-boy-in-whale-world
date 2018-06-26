using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogFrame : MonoBehaviour {

	public string phrase;
	public float minDuration;
	[SerializeField] Text textObject;

	// Use this for initialization
	void Start () {
		textObject.text = phrase;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hide() {
		gameObject.SetActive(false);
	}
	

}
