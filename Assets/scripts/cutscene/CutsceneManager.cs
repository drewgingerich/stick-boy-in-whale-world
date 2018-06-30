using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

	[SerializeField] List<Sprite> sprites;
	[SerializeField] Image image;
	[SerializeField] RectTransform rectT;
	[SerializeField] float speed = 0.5f;

	List<int> possibleMovement = new List<int> {-1, 1};
	Coroutine panRoutine;

	IEnumerator Start () {
		for (int i = 0; i < sprites.Count; i++) {
			image.sprite = sprites[i];
			panRoutine = StartCoroutine(Pan());
			yield return new WaitForSeconds(4);
			StopCoroutine(panRoutine);
		}
		SceneManager.LoadScene("02_Whale");
	}

	IEnumerator Pan() {
		rectT.offsetMax += Vector2.zero;
		rectT.offsetMin += Vector2.zero;
		int xMove = possibleMovement[Random.Range(0, 2)];
		int yMove = possibleMovement[Random.Range(0, 2)];
		while(true) {
			Vector2 movement = new Vector2(xMove, yMove) * Time.deltaTime * speed;
			rectT.offsetMax += movement;
			rectT.offsetMin += movement;
			yield return null;
		}
	}
}
