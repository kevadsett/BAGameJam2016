using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonTest : MonoBehaviour
{
	public Text textOutput;

	public void OnClick()
	{
		Debug.Log( "HEY!" );
		var demonData = DemonDatabase.GetRandomDemon();

		textOutput.text = demonData.Name;
	}
}
