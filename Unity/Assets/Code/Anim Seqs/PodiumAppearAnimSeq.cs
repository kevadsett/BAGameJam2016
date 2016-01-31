using UnityEngine;
using System;

[CreateAssetMenu]
public class PodiumAppearAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenToQueue;

	public void Trigger(AnimateToPoint victimAnimator,
		Transform demonRoot,
		Transform sadRoot,
		Vector3 startPos,
		Vector3 podiumPos,
		Action onComplete) {

		victimAnimator.transform.position = startPos;

		victimAnimator.Trigger(TweenToQueue, podiumPos, () => {
			demonRoot.gameObject.SetActive(false);
			sadRoot.gameObject.SetActive(true);
			onComplete();
		});
	}
}