using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class SimpleTween : ScriptableObject {
	[SerializeField] private float duration;
	[SerializeField] private AnimationCurve timeScale;

	public float Duration {
		get { return duration; }
	}

	public virtual Vector3 Evaluate(Vector3 a, Vector3 b, float time) {
		return Vector3.Lerp(a, b, timeScale.Evaluate(time));
	}
	protected virtual float ScaleTime(float time) {
		return timeScale.Evaluate(time);
	}
}
