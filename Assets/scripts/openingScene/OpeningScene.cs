using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour {

	[SerializeField] List<Sprite> sprites;
	[SerializeField] new SpriteRenderer renderer;
	[SerializeField] new OpeningSceneCamera camera;

	IEnumerator Start () {
		for (int i = 0; i < sprites.Count; i++) {
			renderer.sprite = sprites[i];
			Vector2 cameraMove = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)); 
			camera.Move(cameraMove);
			yield return new WaitForSeconds(4);
		}
		SceneManager.LoadScene(1);
	}
}
