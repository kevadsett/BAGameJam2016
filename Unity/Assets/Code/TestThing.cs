using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestThing : MonoBehaviour {
	[SerializeField] private List<SimpleTween> tweens;
	[SerializeField] private AnimateToPoint animator;

	private bool allowInput = true;

	private List<KeyCode> keyCodes = new List<KeyCode> {
		KeyCode.Q,
		KeyCode.W,
		KeyCode.E,
		KeyCode.R,
		KeyCode.T,
		KeyCode.Y,
	};

	private void Update() {
		if (allowInput && Input.GetMouseButtonDown(0)) {
			int tweenToUse = 0;
			for (int i = 0; i < tweens.Count && i < keyCodes.Count; i++) {
				if (Input.GetKey(keyCodes[i])) {
					tweenToUse = i;	
				}
			}

			var targetPos = Input.mousePosition;
			targetPos.z = 10.0f;
			targetPos = MainCamera.Instance.ScreenToWorldPoint(targetPos);

			allowInput = false;

			animator.Trigger(tweens[tweenToUse], targetPos, () => allowInput = true);
		}
	}
}