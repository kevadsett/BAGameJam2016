﻿using UnityEngine;
using System;

[CreateAssetMenu]
public class PodiumAppearAnimSeq : ScriptableObject
{
	[SerializeField] private SimpleTween TweenToQueue;

	public void Trigger(AnimateToPoint victimAnimator,
		Vector3 startPos,
		Vector3 podiumPos,
		Action onComplete) {

		victimAnimator.transform.position = startPos;

		victimAnimator.Trigger(TweenToQueue, podiumPos, () => {
			onComplete();
		});
	}
}