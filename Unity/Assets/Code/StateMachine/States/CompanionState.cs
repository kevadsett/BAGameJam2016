using System;
using UnityEngine.SceneManagement;

public class CompanionState : State
{
	public override void OnEnter()
	{
		SceneManager.LoadScene ("Companion", LoadSceneMode.Additive);
	}

	public override void OnExit()
	{
		SceneManager.UnloadScene ("Companion");
	}

	public override void OnUpdate()
	{
	}
}
