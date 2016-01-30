using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class CompanionDetailState : State
{
	public static eDemonType DemonType;

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
