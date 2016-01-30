using UnityEngine;
using System;

[CreateAssetMenu]
public class DevilHandAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenDevilHandOn;
	[SerializeField] private SimpleTween TweenDevilHandClose;
	[SerializeField] private SimpleTween TweenDevilHandOff;
	[SerializeField] private Vector3 HandSpacing;
	[SerializeField] private Vector3 OffscreenPos;

	public void Trigger(AnimateToPoint devilHandAnimator1,
						AnimateToPoint devilHandAnimator2,
						AnimateToPoint victimAnimator,
						Vector3 podiumPos,
						Action onComplete) {

		devilHandAnimator1.gameObject.SetActive(true);
		devilHandAnimator2.gameObject.SetActive(true);
		devilHandAnimator1.transform.position = OffscreenPos - HandSpacing;
		devilHandAnimator2.transform.position = OffscreenPos + HandSpacing;
		victimAnimator.transform.position = podiumPos;

		devilHandAnimator1.Trigger(TweenDevilHandOn, podiumPos - HandSpacing);
		devilHandAnimator2.Trigger(TweenDevilHandOn, podiumPos + HandSpacing, () => {
			devilHandAnimator1.Trigger(TweenDevilHandClose, podiumPos);
			devilHandAnimator2.Trigger(TweenDevilHandClose, podiumPos, () => {
				victimAnimator.Trigger(TweenDevilHandOff, OffscreenPos);
				devilHandAnimator1.Trigger(TweenDevilHandOff, OffscreenPos);
				devilHandAnimator2.Trigger(TweenDevilHandOff, OffscreenPos, () => {
					devilHandAnimator1.gameObject.SetActive(false);
					devilHandAnimator2.gameObject.SetActive(false);
					victimAnimator.gameObject.SetActive(false);
					onComplete();
				});
			});
		});
	}
}