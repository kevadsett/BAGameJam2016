using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{
	public void OnClick()
	{
		StateMachine.SetState( eState.Game );
	}
}
