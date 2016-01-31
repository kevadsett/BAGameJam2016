using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputShaker : MonoBehaviour {
	public static InputShaker Instance { get; private set; }

	private class ShakeEffect {
		public float timer;
		public float force;
		public Vector3 normal;
		public Vector3 tangent;
	}

	[SerializeField] AnimationCurve falloff1;
	[SerializeField] AnimationCurve falloff2;
	[SerializeField] float speed;

	private List<ShakeEffect> activeEffects = new List<ShakeEffect>();
	private Vector3 rootPosition;

	private void Awake() {
		Instance = this;
		rootPosition = transform.localPosition;
	}

	private void Update() {

		Vector3 offset = Vector3.zero;

		for (int i = 0; i < activeEffects.Count; i++) {
			var active = activeEffects[i];
			active.timer -= Time.deltaTime * speed;

			float force1 = falloff1.Evaluate(1.0f - active.timer) * active.force;
			float force2 = falloff2.Evaluate(1.0f - active.timer) * active.force;

			offset += active.normal * force1;
			offset += active.tangent * force2;
		}

		activeEffects.RemoveAll(p => p.timer <= 0.0f);

		transform.localPosition = rootPosition + offset;
		transform.localRotation = Quaternion.Euler( new Vector3( 0.0f, 0.0f, offset.x * 0.5f ) );
	}

	public void ApplyShake(float force) {

		var normal = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f), 0.0f).normalized;
		var tangent = Vector3.Cross(normal, Vector3.forward).normalized;

		activeEffects.Add(new ShakeEffect { force = force, timer = 1.0f, normal = normal, tangent = tangent });
	}
}