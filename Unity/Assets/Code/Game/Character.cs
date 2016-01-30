﻿using System;
using UnityEngine;

[RequireComponent(typeof(AnimateToPoint))]
public class Character : MonoBehaviour
{
	[SerializeField] private EnterQueueAnimSeq enterQueueAnims;
	[SerializeField] private PodiumAppearAnimSeq approachPodiumAnims;

	private AnimateToPoint animator;

	public DemonData DemonData { get; private set; }

	public static Character Instantiate(Character prefab, int queuePosition, Transform queue, DemonData demonData) {
		var go = GameObject.Instantiate(prefab.gameObject) as GameObject;
		go.transform.localPosition = queue.position + Vector3.up * 10.0f;

		var character = go.GetComponent<Character>();
		character.animator = go.GetComponent<AnimateToPoint>();
		character.PositionInQueue(queue, queuePosition);
		character.DemonData = demonData;

		return character;
	}

	public void PositionInQueue(Transform queue, int pos) {
		Vector3 targetPos = queue.position + Vector3.left * (float)(pos % 3)
											+ Vector3.up * (float)(pos / 3);

		transform.parent = queue;
		enterQueueAnims.Trigger(animator, transform.position, targetPos, () => {});
	}

	public void PositionInChoir(Transform choir, int pos) {
		Vector3 targetPos = choir.position + Vector3.right * (float)(pos % 3)
											+ Vector3.up * (float)(pos / 3);
		
		transform.parent = choir;
		transform.position = targetPos;
	}

	public void PositionAtPodium(Transform podium) {
		transform.parent = podium;
		approachPodiumAnims.Trigger(animator, transform.position, podium.position, () => {});
	}

	public void PositionInHell(Transform hell) {
		transform.parent = hell;
		transform.position = hell.position;
		gameObject.SetActive(false);
	}
}
