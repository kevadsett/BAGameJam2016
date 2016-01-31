using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultsLogic : MonoBehaviour
{
	public Text Score;

	public GameObject Good;
	public GameObject Evil;

	public static int DemonsExorcised;

	void Awake()
	{
		Score.text = string.Format( "Demons Exorcised: {0}", DemonsExorcised );
	}

	public void OnNext()
	{
		StateMachine.SetState( eState.Frontend );
	}

	void Update()
	{
		MusicManager.Instance.RemoveAllClips();
	}
}
