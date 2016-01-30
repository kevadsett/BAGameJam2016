using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eState
{
	Initialisation,

	Frontend,
	Game,
	Results,
	Companion,
	CompanionDetail
}

public static class StateMachine
{
	static Dictionary< eState, State > States = new Dictionary< eState, State >()
	{
		{ eState.Initialisation, new InitialisationState() },

		{ eState.Frontend, new FrontendState() },
		{ eState.Game, new GameState() },
		{ eState.Results, new ResultsState() },
		{ eState.Companion, new CompanionState() },
		{ eState.CompanionDetail, new CompanionDetailState() }
	};

	static eState currentState;

	static StateMachine()
	{
		currentState = eState.Initialisation;
		States[ currentState ].OnEnter();
	}

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
