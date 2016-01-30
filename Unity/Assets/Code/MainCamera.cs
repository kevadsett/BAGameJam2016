using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour {
	public static Camera Instance { get; private set; }
	private void Awake() {
		Instance = GetComponent<Camera>();
	}
}
