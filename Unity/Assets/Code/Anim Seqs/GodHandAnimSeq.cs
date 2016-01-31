using UnityEngine;
using System;

[CreateAssetMenu]
public class GodHandAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenGodHandOn;
	[SerializeField] private SimpleTween TweenToChoir;
	[SerializeField] private SimpleTween TweenGodHandOff;
	[SerializeField] private Vector3 OffscreenPos;

	public void Trigger(AnimateToPoint openAnimator,
						AnimateToPoint closedAnimator,
						AnimateToPoint victimAnimator,
						Transform sadRoot,
						Transform happyRoot,
						Transform ecstaticRoot,
						Vector3 podiumPos,
						Vector3 choirPos,
						Action onComplete) {

		sadRoot.gameObject.SetActive(false);
		happyRoot.gameObject.SetActive(true);
		openAnimator.gameObject.SetActive(true);
		closedAnimator.gameObject.SetActive(false);

		openAnimator.transform.position = OffscreenPos;
		victimAnimator.transform.position = podiumPos;

		openAnimator.Trigger(TweenGodHandOn, podiumPos, () => {
			openAnimator.gameObject.SetActive(false);
			closedAnimator.gameObject.SetActive(true);
			happyRoot.gameObject.SetActive(false);
			ecstaticRoot.gameObject.SetActive(true);

			closedAnimator.transform.position = openAnimator.transform.position;

			victimAnimator.Trigger(TweenToChoir, choirPos);
			closedAnimator.Trigger(TweenToChoir, choirPos, () => {
				closedAnimator.Trigger(TweenGodHandOff, OffscreenPos, () => {
					closedAnimator.gameObject.SetActive(false);
					happyRoot.gameObject.SetActive(true);
					ecstaticRoot.gameObject.SetActive(false);

					onComplete();
				});
			});
		});
	}
}