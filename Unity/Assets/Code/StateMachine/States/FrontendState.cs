using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontendState : State
{
	public override void OnEnter()
	{
		SceneManager.LoadScene( "Frontend", LoadSceneMode.Additive );
	}

	public override void OnExit()
	{
		SceneManager.UnloadScene( "Frontend" );
	}

	public override void OnUpdate()
	{
	}
}
