using UnityEngine;
using System.Collections;
using System;

public class AnimateToPoint : MonoBehaviour {
	private SimpleTween tween;
	private Vector3 sourcePos;
	private Vector3 targetPos;
	private Action onComplete;
	private float timeScale;
	private float timer;

	public void Trigger(SimpleTween tween, Vector3 point) {
		Trigger(tween, point, () => {});
	}

	public void Trigger(SimpleTween tween, Vector3 point, Action onComplete) {
		this.tween = tween;
		this.onComplete = onComplete;
		sourcePos = transform.position;
		targetPos = point;

		timeScale = 1.0f / tween.Duration;
		timer = 0.0f;
	}

	private void Update() {
		if (tween != null) {
			if (timer > 1.0f) {
				tween = null;
				transform.position = targetPos;
				onComplete();
			} else { 
				transform.position = tween.Evaluate(sourcePos, targetPos, timer);
			}
			timer += timeScale * Time.deltaTime;
		}
	}
}