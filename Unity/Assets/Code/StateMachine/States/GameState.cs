using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : State
{
	public override void OnEnter()
	{
		SceneManager.LoadScene( "Game", LoadSceneMode.Additive );
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
