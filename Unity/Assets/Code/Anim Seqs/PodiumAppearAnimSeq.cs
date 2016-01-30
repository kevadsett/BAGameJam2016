using UnityEngine;
using System;

[CreateAssetMenu]
public class PodiumAppearAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenToPodium;

	public void Trigger(AnimateToPoint victimAnimator,
		Vector3 startPos,
		Vector3 podiumPos,
		Action onComplete) {

		victimAnimator.transform.position = startPos;

		victimAnimator.Trigger(TweenToPodium, podiumPos, () => {
			onComplete();
		});
	}
}