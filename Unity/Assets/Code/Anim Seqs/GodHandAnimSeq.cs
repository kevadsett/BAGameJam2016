using UnityEngine;
using System;

[CreateAssetMenu]
public class GodHandAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenGodHandOn;
	[SerializeField] private SimpleTween TweenToChoir;
	[SerializeField] private SimpleTween TweenGodHandOff;
	[SerializeField] private Vector3 OffscreenPos;

	public void Trigger(AnimateToPoint godHandAnimator,
						AnimateToPoint victimAnimator,
						Vector3 podiumPos,
						Vector3 choirPos,
						Action onComplete) {

		godHandAnimator.gameObject.SetActive(true);
		godHandAnimator.transform.position = OffscreenPos;
		victimAnimator.transform.position = podiumPos;

		godHandAnimator.Trigger(TweenGodHandOn, podiumPos, () => {
			victimAnimator.Trigger(TweenToChoir, choirPos);
			godHandAnimator.Trigger(TweenToChoir, choirPos, () => {
				godHandAnimator.Trigger(TweenGodHandOff, OffscreenPos, () => {
					godHandAnimator.gameObject.SetActive(false);
					onComplete();
				});
			});
		});
	}
}