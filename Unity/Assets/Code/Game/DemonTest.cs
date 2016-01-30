using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonTest : MonoBehaviour
{
	public Text textOutput;

	public void OnClick()
	{
		var demonData = DemonDatabase.GetRandomDemon();

		textOutput.text = demonData.Name;
	}
}
