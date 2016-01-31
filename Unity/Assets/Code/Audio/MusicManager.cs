using System;
using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
	public List<AudioClip> AngelLayers = new List<AudioClip>();
	public List<AudioClip> DemonLayers = new List<AudioClip>();

	public List<AudioSource> AngelSources = new List<AudioSource>();
	public List<AudioSource> DemonSources = new List<AudioSource>();

	public static MusicManager Instance { get; private set; }

	void Awake()
	{
		Instance = this;

		foreach( var layer in AngelLayers )
		{
			AngelSources.Add( CreateSource( layer ) );
		}

		foreach( var layer in DemonLayers )
		{
			DemonSources.Add( CreateSource( layer ) );
		}
	}

	AudioSource CreateSource( AudioClip audioClip )
	{
		var newGO = new GameObject( audioClip.name );
		newGO.transform.parent = transform;

		var audioSource = newGO.AddComponent< AudioSource >();
		audioSource.loop = true;
		audioSource.clip = audioClip;
		audioSource.volume = 0.0f;

		audioSource.Play();

		return audioSource;
	}

	public void AddAngelClip()
	{
		foreach( var angel in AngelSources )
		{
			if( angel.volume > 0.0f )
				continue;

			angel.gameObject.AddComponent< VolumeAnimateUp >();

			return;
		}
	}

	public void AddDemonClip()
	{
		foreach( var demon in DemonSources )
		{
			if( demon.volume > 0.0f )
				continue;

			demon.gameObject.AddComponent< VolumeAnimateUp >();

			return;
		}
	}

	public void RemoveAllClips()
	{
		foreach( var demon in DemonSources )
		{
			demon.volume = 0.0f;
			var anim = demon.GetComponent< VolumeAnimateUp >();
			if( anim != null )
				GameObject.DestroyImmediate( anim );
		}

		foreach( var angel in AngelSources )
		{
			angel.volume = 0.0f;
			var anim = angel.GetComponent< VolumeAnimateUp >();
			if( anim != null )
				GameObject.DestroyImmediate( anim );
		}
	}
}
