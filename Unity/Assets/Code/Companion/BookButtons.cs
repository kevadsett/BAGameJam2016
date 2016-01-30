using UnityEngine;
using System.Collections;

public class BookButtons : MonoBehaviour {

	public void OnAirClick()
	{
		CompanionDetailState.DemonType = eDemonType.Air;
		StateMachine.SetState( eState.CompanionDetail );
	}

	public void OnEarthClick()
	{
		CompanionDetailState.DemonType = eDemonType.Earth;
		StateMachine.SetState( eState.CompanionDetail );
	}

	public void OnFireClick()
	{
		CompanionDetailState.DemonType = eDemonType.Fire;
		StateMachine.SetState( eState.CompanionDetail );
	}

	public void OnWaterClick()
	{
		CompanionDetailState.DemonType = eDemonType.Water;
		StateMachine.SetState( eState.CompanionDetail );
	}
}
