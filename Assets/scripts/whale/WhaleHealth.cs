using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WhaleHealth : MonoBehaviour {

	public UnityEvent OnDie;

	[SerializeField] Slider hpSlider;
	[SerializeField] float health;
	[SerializeField] float hpGainRate;
	[SerializeField] float hpLossRate;

	bool healthy = true;
	bool paused = false;

	public void SetHealthy(bool value) {
		healthy = value;
	}

	public void SetPause(bool value) {
		paused = value;
	}

	void Start() {
		hpSlider.minValue = 0;
		hpSlider.maxValue = health;
		hpSlider.value = health;
	}

	void Update () {
		if (paused)
			return;
		float rate = healthy ? hpGainRate : -hpLossRate;
		health += rate * Time.deltaTime;
		hpSlider.value = Mathf.Max(0, health);
		if (health <= 0)
			Die();
	}

	void Die() {
		gameObject.SetActive(false);
		OnDie.Invoke();
	}
}
