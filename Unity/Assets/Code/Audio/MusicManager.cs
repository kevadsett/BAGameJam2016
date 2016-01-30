using System;
using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
	public List<AudioClip> AngelLayers = new List<AudioClip>();
	public List<AudioClip> DemonLayers = new List<AudioClip>();

	public List<AudioSource> AngelSources = new List<AudioSource>();
	public List<AudioSource> DemonSources = new List<AudioSource>();

	void Start()
	{
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

		//audioSource.Play();

		return audioSource;
	}
}

