using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] Character CharacterPrefab;
	[SerializeField] Transform QueueTransform;

	Queue<Character> Infected = new Queue<Character>();

	List<Character> Cured = new List<Character>();
	List<Character> Failed = new List<Character>();

	Character stageCharacter;

	const float timeBetweenSpawns = 5.0f;
	float spawnTimer = 0.0f;

	const float maxTimeOnStage = 10.0f;
	float stageTimer = 0.0f;

	public InputField inputField;


	public void Update()
	{
		SpawnUpdate();
		StageUpdate();
	}

	void SpawnUpdate()
	{
		spawnTimer -= Time.deltaTime;

		if( spawnTimer < 0.0f )
		{
			SpawnInfected();
			spawnTimer = timeBetweenSpawns;
		}
	}

	void SpawnInfected()
	{
		//	GAVIN TODO: Show character on screen!
		Infected.Enqueue( Character.Instantiate( CharacterPrefab, Infected.Count, QueueTransform ) );
	}

	void StageUpdate()
	{
		stageTimer -= Time.deltaTime;

		if( stageTimer > 0.0f )
			return;

		if( stageCharacter != null )	//	When first in, this is null, as we move a character to stage immediately.
		{
			//	Player Failed to cure in time!
			StageFail();
		}

		//	Move new character onto the Stage

		inputField.text = "";

		if( Infected.Count() == 0 )	//	Make sure the infected queue is never empty when moving things to the Stage area.
		{
			SpawnInfected();
			spawnTimer = timeBetweenSpawns;
		}

		stageCharacter = Infected.Dequeue();
		stageTimer = maxTimeOnStage;

		//	GAVIN TODO - stageCharacter needs to move to the stage area.
		//	This will occuring either due to timeout (within this function) or input of an
		//	incorrect chant (but both will come through THIS code path).
	}

	public void OnInputValueSubmitted()
	{
		bool success = false;
		if( inputField.text == "test" )
			success = true;

		if( success )
		{
			StageSuccess();
		}
		else
		{
			StageFail();
		}
	}

	void StageSuccess()
	{
		Debug.Log( "SUCCESS" );

		spawnTimer = 0.0f;	//	Get another character onto the stage.

		inputField.text = "";
	}

	void StageFail()
	{
		Debug.Log( "FAIL" );

		SpawnInfected();	//	PUNISHMENT!!!

		spawnTimer = 0.0f;	//	Get another character onto the stage.

		inputField.text = "";
	}
}

