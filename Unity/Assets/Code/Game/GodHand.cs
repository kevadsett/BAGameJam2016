using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimateToPoint))]
public class GodHand : MonoBehaviour {
	public static AnimateToPoint OpenAnimator {
		get; private set;
	}
	public static AnimateToPoint ClosedAnimator {
		get; private set;
	}

	[SerializeField] private bool isOpen;

	private void Awake() {
		if (isOpen) {
			OpenAnimator = GetComponent<AnimateToPoint>();
		} else {
			ClosedAnimator = GetComponent<AnimateToPoint>();
		}
		gameObject.SetActive(false);
	}
}
