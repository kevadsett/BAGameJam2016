using System;

public class InitialisationState : State
{
	public override void OnEnter()
	{
		DemonDatabase.LoadDatabase();
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}
}
