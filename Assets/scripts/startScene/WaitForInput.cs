using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForInput : MonoBehaviour {

	void Update() {
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
			LoadNextScene();
	}

	void LoadNextScene() {
		SceneManager.LoadScene("01_OpeningCutscene");
	}
}
