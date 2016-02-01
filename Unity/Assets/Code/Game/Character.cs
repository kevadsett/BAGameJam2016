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
	[SerializeField] private Transform sadRoot;
	[SerializeField] private Transform happyRoot;
	[SerializeField] private Transform ecstaticRoot;
	[SerializeField] private Transform miniDemonRoot;
	[SerializeField] private ParticleSystem poofCloud;

	public float YCutOffOffset = -70.0f;

	GameObject demonGO;

	private AnimateToPoint animator;

	public DemonData DemonData { get; private set; }

	public float DemonAmount { get; set; }

	public static Character Instantiate(Character prefab, int queuePosition, Transform queue, DemonData demonData, GameObject demonGO, Action callback) {
		var go = GameObject.Instantiate(prefab.gameObject) as GameObject;
		go.transform.localPosition = queue.position + Vector3.up * 10.0f;

		var character = go.GetComponent<Character>();
		character.animator = go.GetComponent<AnimateToPoint>();
		character.PositionInQueue(queue, queuePosition, callback);
		character.DemonData = demonData;

		character.demonGO = demonGO;
		demonGO.transform.parent = character.transform;
		demonGO.transform.localPosition = Vector3.zero;
		demonGO.SetActive(false);

		character.DemonAmount = 0.0f;

		var dgo = GameObject.Instantiate(demonData.ArtAsset) as GameObject;
		dgo.transform.parent = character.miniDemonRoot;
		dgo.transform.localPosition = Vector3.zero;

		character.sadRoot.gameObject.SetActive(false);
		character.happyRoot.gameObject.SetActive(false);
		character.ecstaticRoot.gameObject.SetActive(false);

		return character;
	}

	public void PositionInQueue(Transform queue, int pos, Action callback) {
		Vector3 targetPos = queue.position + TargetPosForQueuePos(pos);

		transform.parent = queue;
		enterQueueAnims.Trigger(animator, transform.position, targetPos, callback);
	}

	public void PositionInChoir(Transform choir, int pos, Action callback) {
		sadRoot.gameObject.SetActive(false);
		demonGO.SetActive(false);

		Vector3 targetPos = choir.position + TargetPosForChoirPos(pos);
		
		transform.parent = choir;
		godHandAnimSeq.Trigger(GodHand.OpenAnimator, GodHand.ClosedAnimator, animator,
								sadRoot, happyRoot, ecstaticRoot,
								transform.position, targetPos, callback);
	}

	public void PositionAtPodium(Transform podium, Action callback) {
		transform.parent = podium;
		approachPodiumAnims.Trigger(animator, poofCloud, demonGO, miniDemonRoot, sadRoot, transform.position, podium.position, callback);
	}

	public void PositionInHell(Transform hell, Action callback) {
		transform.parent = hell;
		devilHandAnimSeq.Trigger(DevilHand.LeftAnimator, DevilHand.RightAnimator, animator, transform.position, callback);
	}

	private Vector3 TargetPosForQueuePos(int pos) {
		return Vector3.left * (float)(pos % CharsPerPew)
			+ Vector3.up * ((float)(pos / CharsPerPew) * 0.62f + 0.6f)
			+ Vector3.forward * (float)(pos / CharsPerPew)
			+ Vector3.right * UnityEngine.Random.Range(-0.1f, 0.1f);
	}

	private Vector3 TargetPosForChoirPos(int pos) {
		return Vector3.right * (float)(pos % CharsPerPew)
			+ Vector3.up * ((float)(pos / CharsPerPew) * 0.62f + 0.6f)
			+ Vector3.forward * (float)(pos / CharsPerPew)
			+ Vector3.right * UnityEngine.Random.Range(-0.1f, 0.1f)
			+ Vector3.down * 0.2f;
	}
	
	void Update()
	{
		float alpha = 1.0f;
		if( DemonAmount == 0.0f )
		{
			alpha = 0.0f;
		}

		Vector3 minPos = new Vector3( 0.1f, -4.5f, 0.2f );
		Vector3 maxPos = new Vector3( 0.1f, -1.5f, 0.2f );

		demonGO.transform.localPosition = Vector3.Lerp( minPos, maxPos, DemonAmount );

		var screenPoint = Camera.main.WorldToScreenPoint( transform.position );


		var materialList = demonGO.GetComponent< StageDemon >().DemonMaterials;

		foreach( var materialGO in materialList )
		{
			const float bullshit = -0.1f;

			materialGO.GetComponent<Renderer>().material.SetFloat( "_YCutOff", screenPoint.y + ( Screen.height * bullshit ) );
			materialGO.GetComponent<Renderer>().material.SetFloat( "_Alpha", alpha );
		}
	}
}
