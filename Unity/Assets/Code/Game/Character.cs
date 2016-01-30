using System;
using UnityEngine;

[RequireComponent(typeof(AnimateToPoint))]
public class Character : MonoBehaviour
{
	[SerializeField] private EnterQueueAnimSeq enterQueueAnims;
	[SerializeField] private PodiumAppearAnimSeq approachPodiumAnims;
	[SerializeField] private GodHandAnimSeq godHandAnimSeq;
	[SerializeField] private DevilHandAnimSeq devilHandAnimSeq;
	[SerializeField] private int CharsPerPew;

	private AnimateToPoint animator;

	public DemonData DemonData { get; private set; }

	public static Character Instantiate(Character prefab, int queuePosition, Transform queue, DemonData demonData, Action callback) {
		var go = GameObject.Instantiate(prefab.gameObject) as GameObject;
		go.transform.localPosition = queue.position + Vector3.up * 10.0f;

		var character = go.GetComponent<Character>();
		character.animator = go.GetComponent<AnimateToPoint>();
		character.PositionInQueue(queue, queuePosition, callback);
		character.DemonData = demonData;

		return character;
	}

	public void PositionInQueue(Transform queue, int pos, Action callback) {
		Vector3 targetPos = queue.position + Vector3.left * (float)(pos % CharsPerPew)
			+ Vector3.up * (float)(pos / CharsPerPew)
			+ Vector3.right * (float)((pos / CharsPerPew) % 2) * 0.5f;

		transform.parent = queue;
		enterQueueAnims.Trigger(animator, transform.position, targetPos, callback);
	}

	public void PositionInChoir(Transform choir, int pos, Action callback) {
		Vector3 targetPos = choir.position + Vector3.right * (float)(pos % CharsPerPew)
			+ Vector3.up * (float)(pos / CharsPerPew)
			+ Vector3.right * (float)((pos / CharsPerPew) % 2) * 0.5f;
		
		transform.parent = choir;
		godHandAnimSeq.Trigger(GodHand.GodAnimator, animator, transform.position, targetPos, callback);
	}

	public void PositionAtPodium(Transform podium, Action callback) {
		transform.parent = podium;
		approachPodiumAnims.Trigger(animator, transform.position, podium.position, callback);
	}

	public void PositionInHell(Transform hell, Action callback) {
		transform.parent = hell;
		devilHandAnimSeq.Trigger(DevilHand.LeftAnimator, DevilHand.RightAnimator, animator, transform.position, callback);
	}
}
