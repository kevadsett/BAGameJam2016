using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] Character CharacterPrefab;
	[SerializeField] Transform QueueTransform;
	[SerializeField] Transform PodiumTransform;
	[SerializeField] Transform ChoirTransform;
	[SerializeField] Transform HellTransform;

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

	private bool isInputEnabled = false;

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
		SpawnInfected(() => {});
	}

	void SpawnInfected(Action callback)
	{
		var newCharacter = Character.Instantiate( CharacterPrefab, Infected.Count, QueueTransform, DemonDatabase.GetRandomDemon(), callback );
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
		isInputEnabled = false;

		if( Infected.Count() == 0 )	//	Make sure the infected queue is never empty when moving things to the Stage area.
		{
			SpawnInfected(() => newCharacterOnStagePlaced());
			spawnTimer = timeBetweenSpawns;
		}
		else
		{
			newCharacterOnStagePlaced();
		}

	}

	void newCharacterOnStagePlaced()
	{
		stageCharacter = Infected.Dequeue();
		stageTimer = maxTimeOnStage;

		stageCharacter.PositionAtPodium(PodiumTransform, () => isInputEnabled = true);

		for( int i = 0; i < Infected.Count; i++ )
		{
			Infected.ElementAt(i).PositionInQueue(QueueTransform, i, () => {});
		}
	}

	public void OnInputValueSubmitted()
	{
		if( !isInputEnabled )
			return;

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

		isInputEnabled = false;

		stageCharacter.PositionInChoir( ChoirTransform, Cured.Count, newCharacterOnStage );
		Cured.Add( stageCharacter );
	}

	void StageFail()
	{
		Debug.Log( "FAIL" );

		isInputEnabled = false;

		stageCharacter.PositionInHell( HellTransform, newCharacterOnStage);
		Failed.Add( stageCharacter );

		SpawnInfected();	//	PUNISHMENT!!!
		SpawnInfected();
		SpawnInfected();

		AudioManager.Instance.Play( "Demon_1" );
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

