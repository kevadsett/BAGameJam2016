using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
	public void OnGameClick()
	{
		StateMachine.SetState( eState.Game );
	}

	public void OnCompanionClick()
	{
		SceneManager.LoadScene ("CompanionDemonDisplay");
	}
}
