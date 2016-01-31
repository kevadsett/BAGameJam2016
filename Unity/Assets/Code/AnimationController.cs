using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	/*[SerializeField] private AnimateToPoint godHandAnimator;
	[SerializeField] private AnimateToPoint devilHandAnimator1;
	[SerializeField] private AnimateToPoint devilHandAnimator2;
	[SerializeField] private AnimateToPoint victimAnimator;

	[SerializeField] private PodiumAppearAnimSeq podiumAnims;
	[SerializeField] private DevilHandAnimSeq devilHandAnims;
	[SerializeField] private GodHandAnimSeq godHandAnims;

	private enum State { Queue, Podium, Choir, Hell, Animating };
	private State MyState;

	private Vector3 queuePos = Vector3.left * 5.0f;
	private Vector3 podiumPos = Vector3.zero;
	private Vector3 choirPos = Vector3.right * 5.0f;
	private Vector3 godHandPos = Vector3.up * 5.0f;
	private Vector3 devilHandPos = Vector3.down * 5.0f;

	public void Initialize() {
		MyState = State.Queue;
		victimAnimator.transform.position = queuePos;
		victimAnimator.gameObject.SetActive(true);
		godHandAnimator.gameObject.SetActive(false);
		devilHandAnimator1.gameObject.SetActive(false);
		devilHandAnimator2.gameObject.SetActive(false);
	}

	public void QueueToPodium() {
		MyState = State.Animating;
		podiumAnims.Trigger(victimAnimator, queuePos, podiumPos, () => {
			MyState = State.Podium;
		});
	}

	public void PodiumToChoir() {
		MyState = State.Animating;
		godHandAnims.Trigger(godHandAnimator, godHandAnimator, victimAnimator, podiumPos, choirPos, () => {
			MyState = State.Choir;
		});
	}

	public void PodiumToHell() {
		MyState = State.Animating;
		devilHandAnims.Trigger(devilHandAnimator1, devilHandAnimator2, victimAnimator, podiumPos, () => {
			MyState = State.Hell;
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
	}*/
}
