using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScene : MonoBehaviour {

	public bool startAutomatically = false;
	[SerializeField] List<DialogFrame> frames = new List<DialogFrame>();

	// Use this for initialization
	void Start () {
		for( int i = 0; i < gameObject.transform.GetChildCount(); i++ ) {
			frames.Add( gameObject.transform.GetChild(i).GetComponent<DialogFrame>() );
			Debug.Log( gameObject.transform.GetChild(i) );
			frames[i].gameObject.SetActive(false);
		}
		if( startAutomatically ) {
			StartCoroutine( DialogRoutine() );
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartDialog() {

	}

	IEnumerator DialogRoutine() {
		//dialog started
		for( int i = 0; i < frames.Count; i++ ) {
			// Debug.Log( frames[i].gameObject );
			frames[i].gameObject.SetActive(true);
			while( !Input.GetMouseButtonDown(0) ) {
				yield return null;
			}
			yield return null;
			frames[i].Hide();
		}
		//dialog ended
	}
}
