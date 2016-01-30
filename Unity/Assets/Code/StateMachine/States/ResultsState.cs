using System;
using UnityEngine.SceneManagement;

public class ResultsState : State
{
	public override void OnEnter()
	{
		SceneManager.LoadScene( "Results", LoadSceneMode.Additive );
	}

	public override void OnExit()
	{
		SceneManager.UnloadScene( "Results" );
		SceneManager.UnloadScene( "Game" );
	}

	public override void OnUpdate()
	{
	}
}
