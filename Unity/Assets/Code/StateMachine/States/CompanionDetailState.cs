using System;
using UnityEngine.SceneManagement;

public class CompanionDetailState : State
{
	public override void OnEnter()
	{
		SceneManager.LoadScene ("CompanionDetail", LoadSceneMode.Additive);
	}

	public override void OnExit()
	{
		SceneManager.UnloadScene ("CompanionDetail");
	}

	public override void OnUpdate()
	{
	}
}
