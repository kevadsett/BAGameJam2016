using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	[SerializeField] Character CharacterPrefab;
	[SerializeField] GameObject DemonPrefab;
	[SerializeField] Transform QueueTransform;
	[SerializeField] Transform PodiumTransform;
	[SerializeField] Transform ChoirTransform;
	[SerializeField] Transform HellTransform;

	Queue<Character> Infected = new Queue<Character>();

	public List<Character> Cured = new List<Character>();
	List<Character> Failed = new List<Character>();

	Character stageCharacter;

	const float timeBetweenSpawns = 5.0f;
	float spawnTimer = 0.0f;

	const float maxTimeOnStage = 12.0f;
	float stageTimer = 0.0f;

	public InputField inputField;

	public Text debugText;

	public Text DemonName;

	bool IsPlaying = true;
	bool IsCountingDown = false;


	public void Update()
	{
		if( !IsPlaying )
			return;

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
        if( !IsPlaying )
			return;

		var demon = GameObject.Instantiate( DemonPrefab ) as GameObject;

		var newCharacter = Character.Instantiate( CharacterPrefab, Infected.Count, QueueTransform, DemonDatabase.GetRandomDemon(), demon, callback );
		Infected.Enqueue( newCharacter );

		const int maxInfected = 12;
		if( Infected.Count() > maxInfected )
		{
			IsPlaying = false;

			StateMachine.SetState( eState.Results );

			ResultsLogic.DemonsExorcised = Cured.Count;
		}
	}

	void StageUpdate()
	{
		if( IsCountingDown )	
			stageTimer -= Time.deltaTime;

		if( stageCharacter != null )
		{
			float ratio = stageTimer / maxTimeOnStage;
			stageCharacter.DemonAmount = 1.0f - ratio;
		}

		if( stageTimer > 0.0f )
			return;

		if( stageCharacter != null )	//	When first in, this is null, as we move a character to stage immediately.
		{
			//	Player Failed to cure in time!
			StageFail();
		}
		else
		{
			newCharacterOnStage();
		}

		stageTimer = maxTimeOnStage;
	}

	void newCharacterOnStage()
	{
		DemonName.text = "";

		inputField.gameObject.SetActive( false );
		inputField.text = "";
		inputField.readOnly = true;

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

	void SetInputFieldFocus()
	{
		EventSystem.current.SetSelectedGameObject(inputField.gameObject);
		inputField.OnPointerClick(new PointerEventData( EventSystem.current ) );
	}

	void newCharacterOnStagePlaced()
	{
		stageCharacter = Infected.Dequeue();
		stageTimer = maxTimeOnStage;
		IsCountingDown = true;

		stageCharacter.PositionAtPodium(PodiumTransform, () =>
			{
				inputField.gameObject.SetActive( true );
				inputField.readOnly = false;
				SetInputFieldFocus();
			} );
				

		for( int i = 0; i < Infected.Count; i++ )
		{
			Infected.ElementAt(i).PositionInQueue(QueueTransform, i, () => {});
		}

		DemonName.text = stageCharacter.DemonData.Type.ToString() + " " + stageCharacter.DemonData.Name;
	}

	public void OnInputValueSubmitted()
	{
		if( inputField.readOnly )
			return;

		IsCountingDown = false;

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

		inputField.readOnly = true;
		inputField.gameObject.SetActive( false );

		stageCharacter.PositionInChoir( ChoirTransform, Cured.Count, newCharacterOnStage );
		Cured.Add( stageCharacter );
	}

	void StageFail()
	{
		Debug.Log( "FAIL" );

		inputField.readOnly = true;
		inputField.gameObject.SetActive( false );

		stageCharacter.PositionInHell( HellTransform, newCharacterOnStage);
		Failed.Add( stageCharacter );

		SpawnInfected();	//	PUNISHMENT!!!
		SpawnInfected();

		AudioManager.Instance.Play( "Demon_1" );

		DemonName.text = "";
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

