using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

	public string targetScene;
	public void Go() {
		SceneManager.LoadScene(targetScene);
	}
}
