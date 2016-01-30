using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	[SerializeField] private AnimateToPoint godHandAnimator;
	[SerializeField] private AnimateToPoint devilHandAnimator1;
	[SerializeField] private AnimateToPoint devilHandAnimator2;
	[SerializeField] private AnimateToPoint victimAnimator;
	[SerializeField] private SimpleTween TweenToPodium;
	[SerializeField] private SimpleTween TweenGodHandOn;
	[SerializeField] private SimpleTween TweenToChoir;
	[SerializeField] private SimpleTween TweenGodHandOff;
	[SerializeField] private SimpleTween TweenDevilHandOn;
	[SerializeField] private SimpleTween TweenDevilHandClose;
	[SerializeField] private SimpleTween TweenDevilHandOff;

	private enum State { Queue, Podium, Choir, Hell, Animating };
	private State MyState;

	private Vector3 queuePos = Vector3.left * 5.0f;
	private Vector3 podiumPos = Vector3.zero;
	private Vector3 choirPos = Vector3.right * 5.0f;
	private Vector3 godHandPos = Vector3.up * 5.0f;
	private Vector3 devilHandPos = Vector3.down * 5.0f;
	private Vector3 devilHandOffset = Vector3.right * 2.0f;

	public void Initialize() {
		MyState = State.Queue;
		godHandAnimator.transform.position = godHandPos;
		victimAnimator.transform.position = queuePos;
		devilHandAnimator1.transform.position = devilHandPos - devilHandOffset;
		devilHandAnimator2.transform.position = devilHandPos + devilHandOffset;
	}

	public void QueueToPodium() {
		Debug.Log("QueueToPodium()");
		MyState = State.Animating;
		victimAnimator.Trigger(TweenToPodium, podiumPos, () => {
			MyState = State.Podium;
		});
	}

	public void PodiumToChoir() {
		Debug.Log("PodiumToChoir()");
		MyState = State.Animating;
		godHandAnimator.Trigger(TweenGodHandOn, podiumPos, () => {
			victimAnimator.Trigger(TweenToChoir, choirPos);
			godHandAnimator.Trigger(TweenToChoir, choirPos, () => {
				godHandAnimator.Trigger(TweenGodHandOff, godHandPos, () => {
					MyState = State.Choir;
				});
			});
		});
	}

	public void PodiumToHell() {
		Debug.Log("PodiumToHell()");
		MyState = State.Animating;
		devilHandAnimator1.Trigger(TweenDevilHandOn, podiumPos - devilHandOffset);
		devilHandAnimator2.Trigger(TweenDevilHandOn, podiumPos + devilHandOffset, () => {
			devilHandAnimator1.Trigger(TweenDevilHandClose, podiumPos);
			devilHandAnimator2.Trigger(TweenDevilHandClose, podiumPos, () => {
				victimAnimator.Trigger(TweenDevilHandOff, devilHandPos);
				devilHandAnimator1.Trigger(TweenDevilHandOff, devilHandPos);
				devilHandAnimator2.Trigger(TweenDevilHandOff, devilHandPos, () => {
					MyState = State.Hell;
				});
			});
		});
	}

	private void Update() {
		if (MyState == State.Queue && Input.GetKeyDown(KeyCode.Alpha1)) {
			QueueToPodium();
		}

		if (MyState == State.Podium && Input.GetKeyDown(KeyCode.Alpha2)) {
			PodiumToChoir();
		}

		if (MyState == State.Podium && Input.GetKeyDown(KeyCode.Alpha3)) {
			PodiumToHell();
		}

		if (MyState != State.Animating && Input.GetKeyDown(KeyCode.R)) {
			Initialize();
		}
	}

	private void Awake() {
		Initialize();
	}
}
