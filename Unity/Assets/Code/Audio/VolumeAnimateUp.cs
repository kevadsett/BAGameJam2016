using System;
using UnityEngine;

public class VolumeAnimateUp : MonoBehaviour
{
	float volume = 0.0f;

	AudioSource audioSource;

	void Awake()
	{
		audioSource = gameObject.GetComponent< AudioSource >();
	}

	void Update()
	{
		const float maxVolume = 0.7f;
		const float fadeUpTime = 1.5f;

		volume += ( 1.0f / fadeUpTime ) * Time.deltaTime;

		volume = Mathf.Clamp( volume, 0.0f, maxVolume );

		audioSource.volume = volume;

		if( volume >= maxVolume )
			GameObject.Destroy( this );
	}
}
