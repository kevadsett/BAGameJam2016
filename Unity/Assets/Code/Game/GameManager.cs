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
	[SerializeField] float TypingShakeForce;

	Queue<Character> Infected = new Queue<Character>();

	public List<Character> Cured = new List<Character>();
	List<Character> Failed = new List<Character>();

	Character stageCharacter;

	float timeBetweenSpawns = 3.0f;
	float spawnTimer = 0.0f;

	float maxTimeOnStage = 12.0f;
	float stageTimer = 0.0f;

	public InputField inputField;

	public Text debugText;

	public Text DemonName;

	bool IsPlaying = true;
	bool IsCountingDown = false;

	const bool debugMode = false;


	public const int maxCharacters = 12;

	void Awake()
	{
		timeBetweenSpawns = 5.0f;
		maxTimeOnStage = 12.0f;
	}

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
		if( IsCountingDown )
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

		var myDemonData = DemonDeck.Draw();

		var demon = GameObject.Instantiate( DemonPrefab ) as GameObject;
		demon.GetComponent< DemonRoot >().ActivateDemon( myDemonData.Type );

		var newCharacter = Character.Instantiate( CharacterPrefab, Infected.Count, QueueTransform, myDemonData, demon, callback );
		Infected.Enqueue( newCharacter );

		if( Infected.Count() > maxCharacters )
		{
			GameOver( false );
		}
	}

	void GameOver( bool success )
	{
		IsPlaying = false;

		MusicManager.Instance.RemoveAllClips();

		StateMachine.SetState( eState.Results );

		ResultsLogic.DemonsExorcised = Cured.Count;
		ResultsLogic.Success = success;
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
		if(!IsPlaying)
			return;

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
		if(!IsPlaying)
			return;

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

		DemonName.text = stageCharacter.DemonData.Name.ToUpperInvariant();
	}

	public void OnInputValueChanged()
	{
		if(!IsPlaying)
			return;

		if( inputField.readOnly )
			return;

		if( !inputField.text.EndsWith( " " )  && inputField.text.Length > 1 )
			return;

		Shake();
	}

	void Shake()
	{
		ScreenShaker.Instance.ApplyShake(TypingShakeForce);
	}

	public void OnInputValueSubmitted()
	{
		if(!IsPlaying)
			return;

		if( inputField.readOnly )
			return;

		bool success = false;
		if( inputField.text.ToUpperInvariant() == stageCharacter.DemonData.Chant.ToUpperInvariant()
		 || ( debugMode && inputField.text == "test" ) )
			success = true;
		
		if( success )
		{
			StageSuccess();

			IsCountingDown = false;
			stageCharacter = null;
		}
		else
		{
			InputShaker.Instance.ApplyShake( 40.0f );
			SetInputFieldFocus();
		}
	}

	void StageSuccess()
	{
		Debug.Log( "SUCCESS" );

		maxTimeOnStage -= 0.5f;

		DemonName.text = "";

		if( stageCharacter.DemonData.ChantAudio != null )
			AudioManager.Instance.Play( stageCharacter.DemonData.ChantAudio );

		MusicManager.Instance.AddAngelClip();

		inputField.readOnly = true;
		inputField.gameObject.SetActive( false );

		stageCharacter.DemonAmount = 0.0f;
		stageCharacter.PositionInChoir( ChoirTransform, Cured.Count, newCharacterOnStage );
		Cured.Add( stageCharacter );

		if( Cured.Count >= maxCharacters )
			GameOver( true );

		AudioManager.Instance.Play( "GodHand");
	}

	void StageFail()
	{
		Debug.Log( "FAIL" );

		maxTimeOnStage += 1.0f;

		DemonName.text = "";

		MusicManager.Instance.AddDemonClip();

		Shake();

		inputField.readOnly = true;
		inputField.gameObject.SetActive( false );

		stageCharacter.DemonAmount = 0.0f;
		stageCharacter.PositionInHell( HellTransform, newCharacterOnStage);
		Failed.Add( stageCharacter );

		SpawnInfected();	//	PUNISHMENT!!!
		SpawnInfected();

		AudioManager.Instance.Play( "DevilHand" );

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

