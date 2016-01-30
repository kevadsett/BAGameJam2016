using UnityEngine;
using System.Collections;

public enum eDemonType
{
	Pineapple,
	Melon
}

[CreateAssetMenu]
public class DemonData : ScriptableObject
{
	public eDemonType Type;

	public string Name;

	public GameObject ArtAsset;
}
