using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimateToPoint))]
public class GodHand : MonoBehaviour {
	public static AnimateToPoint GodAnimator {
		get; private set;
	}

	private void Awake() {
		GodAnimator = GetComponent<AnimateToPoint>();
		gameObject.SetActive(false);
	}
}
