using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Flasher : MonoBehaviour {
	public static Flasher Instance {
		get; private set;
	}

	[SerializeField] private float flashDuration;

	private Color flashColour;
	private Material targetMat;
	private Renderer renderer;

	public void Flash(Color color) {
		flashColour = color * 0.5f;
	}

	private void Awake() {
		Instance = this;
		renderer = GetComponent<Renderer>();
		targetMat = new Material(renderer.material);
		targetMat.color = Color.clear;
		renderer.sharedMaterial = targetMat;
	}

	private void Update() {
		flashColour.a = Mathf.Clamp01(flashColour.a - Time.deltaTime / flashDuration);
		targetMat.SetColor("_TintColor", flashColour);
	}
}
