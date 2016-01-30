using UnityEngine;
using System;

[CreateAssetMenu]
public class EnterQueueAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenToQueue;

	public void Trigger(AnimateToPoint victimAnimator,
		Vector3 startPos,
		Vector3 queuePos,
		Action onComplete) {

		victimAnimator.transform.position = startPos;

		victimAnimator.Trigger(TweenToQueue, queuePos, () => {
			onComplete();
		});
	}
}