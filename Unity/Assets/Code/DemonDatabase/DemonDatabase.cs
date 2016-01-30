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