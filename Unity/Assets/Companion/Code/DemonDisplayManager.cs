using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DemonDisplayManager : MonoBehaviour {
	private List<DemonData> _demons;
	private DemonData _currentDemon;
	private int _demonIndex = -1;
	void Start () {
		DemonDatabase.LoadDatabase ();
		_demons = DemonDatabase.GetAllDemons ();
		NextDemon ();
	}

	void NextDemon() {
		_demonIndex++;
		if (_currentDemon) {
			Destroy (_currentDemon);
		}
		_currentDemon = _demons [_demonIndex];
		InstantiateCurrentDemon ();
	}

	void InstantiateCurrentDemon() {
		GameObject newDemonObject = Instantiate (_currentDemon.ArtAsset) as GameObject;

		// TODO: REPLACE WITH PREFAB CODE ONCE ASSETS ARE 3D
		newDemonObject.transform.SetParent(GameObject.Find("DemonCanvas").transform);
		newDemonObject.transform.Find ("Name").GetComponent<Text> ().text = _currentDemon.Name;
		newDemonObject.transform.position = Vector3.zero;
	}
}
