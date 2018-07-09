using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimWhaleLights : MonoBehaviour {

	[SerializeField] SpriteRenderer whaleFloorSprite;
	[SerializeField] SpriteRenderer whaleWallTopSprite;

	Color dimColor = new Color32(128, 125, 156, 255);

	public void DimLights() {
		whaleFloorSprite.color = dimColor;
		whaleWallTopSprite.color = dimColor;
	}
}
