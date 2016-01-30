using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EntryPoint : MonoBehaviour
{
	void Awake()
	{
		StateMachine.SetState( eState.Frontend );
	}

	void Update()
	{
		StateMachine.Update();
	}
}
