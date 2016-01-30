using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimateToPoint))]
public class DevilHand : MonoBehaviour {
	public static AnimateToPoint LeftAnimator {
		get; private set;
	}

	public static AnimateToPoint RightAnimator {
		get; private set;
	}

	[SerializeField] private bool isLeftHand;

	private void Awake() {
		if (isLeftHand) {
			LeftAnimator = GetComponent<AnimateToPoint>();
		} else {
			RightAnimator = GetComponent<AnimateToPoint>();
		}
		gameObject.SetActive(false);
	}
}