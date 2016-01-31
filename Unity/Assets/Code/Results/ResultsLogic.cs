using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultsLogic : MonoBehaviour
{
	public Text Score;

	public GameObject Good;
	public GameObject Evil;

	public static bool Success = false;
	public static int DemonsExorcised;

	void Awake()
	{
		if( Success )
		{
			Score.text = string.Format( "MAXIMUM Souls Saved", DemonsExorcised );

			Good.SetActive( true );
			Evil.SetActive( false );
		}
		else
		{
			Score.text = string.Format( "Souls Saved: {0}", DemonsExorcised );

			Good.SetActive( false );
			Evil.SetActive( true );
		}
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
