using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class DemonDatabase
{
	static Dictionary<string, DemonData> Demons;

	public static void LoadDatabase()
	{
		Demons = new Dictionary<string, DemonData>();

		var demonObjects = Resources.LoadAll( "DemonDatabase" );

		foreach ( var demonObject in demonObjects )
		{
			var demonData = demonObject as DemonData;

			Demons.Add( demonData.Name, demonData );
		}
	}

	public static DemonData GetRandomDemon()
	{
		return Demons.Values.ToList()[ Random.Range( 0, Demons.Count() ) ];
	}

	public static DemonData GetDemonByName( string demonName )
	{
		return Demons[ demonName ];
	}

	public static List<DemonData> GetAllDemons()
	{
		return Demons.Values.ToList();
	}

	public static List<DemonData> GetAllDemonsOfType( eDemonType type )
	{
		return GetAllDemons().Where( q => q.Type == type ).ToList();
	}
}

public static class DemonDeck
{
	public static Queue<DemonData> Deck = new Queue<DemonData>();

	public static void Shuffle()
	{
		Deck = new Queue<DemonData>();

		var unshuffledDeck = DemonDatabase.GetAllDemons();

		while( unshuffledDeck.Count > 0 )
			Deck.Enqueue( GetRandomOutOfList( unshuffledDeck ) );
	}

	static DemonData GetRandomOutOfList( List<DemonData> list )
	{
		int element = (int)( Random.value * 1000 ) % list.Count;
		var demon = list[ element ];
		list.RemoveAt( element );

		return demon;
	}

	public static DemonData Draw()
	{
		if( Deck.Count == 0 )
			Shuffle();

		return Deck.Dequeue();
	}
}