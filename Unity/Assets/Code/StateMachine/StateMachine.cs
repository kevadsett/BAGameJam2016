using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eState
{
	None,

	Frontend,
	Game,
	Results
}

public static class StateMachine
{
	static Dictionary< eState, State > States = new Dictionary< eState, State >()
	{
		{ eState.None, new DummyState() },

		{ eState.Frontend, new FrontendState() },
		{ eState.Game, new GameState() },
		{ eState.Results, new ResultsState() }
	};

	static eState currentState = eState.None;

	static public void SetState( eState nextState )
	{
		States[ currentState ].OnExit();
		currentState = nextState;
		States[ currentState ].OnEnter();
	}

	static public void Update()
	{
		States[ currentState ].OnUpdate();
	}
}
