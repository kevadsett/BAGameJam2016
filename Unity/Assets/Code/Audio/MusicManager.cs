using System;
using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
	public List<AudioClip> Layers = new List<AudioClip>();

	void Start()
	{		
		foreach( var layer in Layers )
		{
			var newGO = new GameObject( layer.name );
			newGO.transform.parent = transform;

			var audioSource = newGO.AddComponent< AudioSource >();
			audioSource.loop = true;
			audioSource.clip = layer;

			audioSource.Play();
		}
	}
}

