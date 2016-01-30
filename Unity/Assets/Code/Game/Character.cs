using System;
using UnityEngine;

[RequireComponent(typeof(AnimateToPoint))]
public class Character : MonoBehaviour
{
	[SerializeField] private EnterQueueAnimSeq enterQueueAnims;

	private enum State { Offscreen, Queue, Stage, Choir, Animating };
	private State state;

	private AnimateToPoint animator;

	public static Character Instantiate(Character prefab, int queuePosition, Transform parent) {
		var go = GameObject.Instantiate(prefab.gameObject) as GameObject;

		go.transform.parent = parent;
		go.transform.localPosition = parent.position + Vector3.up * 10.0f;;

		var character = go.GetComponent<Character>();
		character.animator = go.GetComponent<AnimateToPoint>();
		character.AnimateIntoQueuePosition(queuePosition);

		return character;
	}

	public void AnimateIntoQueuePosition(int pos) {
		Vector3 targetPos = transform.position + Vector3.right * (float)(pos % 3)
											   + Vector3.up * (float)(pos / 3);
		state = State.Animating;
		enterQueueAnims.Trigger(animator, transform.position, targetPos, () => {
			state = State.Queue;
		});
	}
}