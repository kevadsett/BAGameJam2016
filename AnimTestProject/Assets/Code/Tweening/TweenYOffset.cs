using System;
using UnityEngine;

[CreateAssetMenu]
public class TweenYOffset : SimpleTween {
	[SerializeField] private AnimationCurve heightOffset;
	[SerializeField] private float heightScale;

	public override Vector3 Evaluate(Vector3 a, Vector3 b, float time) {
		time = ScaleTime(time);
		float offset = heightOffset.Evaluate(time) * heightScale;
		return Vector3.Lerp(a, b, time) + Vector3.up * offset;
	}
}