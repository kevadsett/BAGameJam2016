using UnityEngine;
using System;

[CreateAssetMenu]
public class PodiumAppearAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenToQueue;

	public void Trigger(AnimateToPoint victimAnimator,
		GameObject bigDemonRoot,
		Transform demonRoot,
		Transform sadRoot,
		Vector3 startPos,
		Vector3 podiumPos,
		Action onComplete) {

		demonRoot.gameObject.SetActive(false);
		victimAnimator.transform.position = startPos;

		victimAnimator.Trigger(TweenToQueue, podiumPos, () => {
			bigDemonRoot.SetActive(true);
			sadRoot.gameObject.SetActive(true);
			onComplete();
		});
	}
}