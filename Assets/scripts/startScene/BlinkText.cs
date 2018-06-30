using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

	[SerializeField] Text text;
	[SerializeField] float onTime;
	[SerializeField] float offTime;

	IEnumerator Start () {
		while (true) {
			text.enabled = true;
			yield return new WaitForSeconds(onTime);
			text.enabled = false;
			yield return new WaitForSeconds(offTime);
		}
	}

}
