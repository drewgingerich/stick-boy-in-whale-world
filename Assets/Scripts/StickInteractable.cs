using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickInteractable : MonoBehaviour {
	public UnityEvent OnPokedByStick;
	public GameObject onHitSparks;

	void OnCollisionEnter2D(Collision2D collision) {
		if( collision.gameObject.CompareTag("Stick") ) {
			HitByStick();
		}
	}

	/// <summary>
	/// Notifies that we've been hit! 🚢
	/// </summary>
	/// <param name="hit">Look at the documentation of <see cref="PlayerSwingStick.GetFirstObjHitByStick"/> for more details</param>
	public void HitByStick( RaycastHit2D hit = new RaycastHit2D() ) {
		OnPokedByStick.Invoke();
		// SendMessage("Poked");
		if( onHitSparks != null ) {
			Instantiate( onHitSparks, hit.point, onHitSparks.transform.rotation );
		}
		Debug.Log( "Yarr, " + gameObject + " has been hit by a stick!" );
	}

}
