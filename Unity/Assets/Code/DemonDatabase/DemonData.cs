using UnityEngine;
using System.Collections;

public enum eDemonType
{
	Air,
	Earth,
	Fire,
	Water
}

[CreateAssetMenu]
public class DemonData : ScriptableObject
{
	public eDemonType Type;

	public string Name;

	public GameObject ArtAsset;

	public string Chant;
}
