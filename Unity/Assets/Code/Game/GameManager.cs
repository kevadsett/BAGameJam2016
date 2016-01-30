﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	Queue<Character> Infected = new Queue<Character>();

	List<Character> Cured = new List<Character>();
	List<Character> Failed = new List<Character>();

	Character stageCharacter;

	const float timeBetweenSpawns = 5.0f;
	float spawnTimer = 0.0f;

	const float maxTimeOnStage = 10.0f;
	float stageTimer = 0.0f;

	public InputField inputField;

	public Text debugText;

	public Text DemonName;


	public void Update()
	{
		SpawnUpdate();
		StageUpdate();
		DebugUpdate();
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
		var newCharacter = new Character( DemonDatabase.GetRandomDemon() );

		//	GAVIN TODO: Show character on screen!
		Infected.Enqueue( newCharacter );

		DemonName.text = newCharacter.DemonData.Name;
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

		newCharacterOnStage();
	}

	void newCharacterOnStage()
	{
		inputField.text = "";

		if( Infected.Count() == 0 )	//	Make sure the infected queue is never empty when moving things to the Stage area.
		{
			SpawnInfected();
			spawnTimer = timeBetweenSpawns;
		}

		stageCharacter = Infected.Dequeue();
		stageTimer = maxTimeOnStage;

		//	GAVIN TODO - stageCharacter needs to move to the stage area.
	}

	public void OnInputValueSubmitted()
	{
		bool success = false;
		if( inputField.text == stageCharacter.DemonData.Chant )
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

		//	GAVIN TODO: Move character to the angel area.

		Cured.Add( stageCharacter );

		newCharacterOnStage();
	}

	void StageFail()
	{
		Debug.Log( "FAIL" );

		//	GAVIN TODO: Move character to the demon area.

		Failed.Add( stageCharacter );

		SpawnInfected();	//	PUNISHMENT!!!
		SpawnInfected();
		SpawnInfected();

		newCharacterOnStage();
	}

	void DebugUpdate()
	{
		string debugString = string.Empty;
		debugString += string.Format( "spawnTimer = {0}\n", spawnTimer );
		debugString += string.Format( "stageTimer = {0}\n", stageTimer );

		if( stageCharacter == null )
			debugString += string.Format( "stageCharacter = NULL\n" );
		else
			debugString += string.Format( "stageCharacter = {0}\n", stageCharacter.DemonData.Name );

		debugString += string.Format( "Infected.Count() = {0}\n", Infected.Count() );
		debugString += string.Format( "Cured.Count() = {0}\n", Cured.Count() );
		debugString += string.Format( "Failed.Count() = {0}\n", Failed.Count() );

		debugText.text = debugString;
	}
}

