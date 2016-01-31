using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	public Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();

	AudioSource audioSource;

	void Awake()
	{
		Instance = this;

		audioSource = GetComponent<AudioSource>();

		var audioObjects = Resources.LoadAll( "Audio" );
		foreach( var audioObject in audioObjects )
		{
			var audioClip = audioObject as AudioClip;

			AudioClips.Add( audioClip.name, audioClip );
		}
	}

	public void Play( string clipName, AudioSource myAudioSource )
	{
		myAudioSource.PlayOneShot( AudioClips[ clipName ] );
	}

	public void Play( string clipName )
	{
		audioSource.PlayOneShot( AudioClips[ clipName ] );
	}

	public void Play( AudioClip audioClip )
	{
		audioSource.PlayOneShot( audioClip );
	}

	public void Play (string clipName, float delay) {
		audioSource.clip = AudioClips [clipName];
		audioSource.PlayDelayed (delay);
	}
}
