using System;
using UnityEngine;

public class DemonRoot : MonoBehaviour
{
	public GameObject Air;
	public GameObject Earth;
	public GameObject Fire;
	public GameObject Water;

	public void ActivateDemon( eDemonType demonType )
	{
		Air.SetActive( false );
		Earth.SetActive( false );
		Fire.SetActive( false );
		Water.SetActive( false );

		switch( demonType )
		{
		case eDemonType.Air:
			Air.SetActive( true );
			break;
		case eDemonType.Earth:
			Earth.SetActive( true );
			break;
		case eDemonType.Fire:
			Fire.SetActive( true );
			break;
		case eDemonType.Water:
			Water.SetActive( true );
			break;
		}
	}
}

