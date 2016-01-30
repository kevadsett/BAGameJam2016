using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DemonDisplayManager : MonoBehaviour {
	private List<DemonData> _demons;
	private DemonData _currentDemon;
	private GameObject _currentDemonInstance;
	private int _demonIndex = -1;
	void Start () {
		DemonDatabase.LoadDatabase ();
		_demons = DemonDatabase.GetAllDemonsOfType (CompanionDetailState.DemonType);
		NextDemon ();
	}

	public void NextDemon() {
		_demonIndex = (_demonIndex + 1) % _demons.Count;
		UpdateDemon ();
	}

	public void PreviousDemon() {
		_demonIndex = (_demonIndex + _demons.Count - 1) % _demons.Count;
		UpdateDemon ();
	}

	public void BackToCompanionState() {
		StateMachine.SetState( eState.Companion );
	}

	void RemoveExistingDemonInstance() {
		if (_currentDemonInstance) {
			Destroy (_currentDemonInstance);
		}
	}

	void UpdateDemon() {
		_currentDemon = _demons [_demonIndex];
		RemoveExistingDemonInstance ();
		InstantiateCurrentDemon ();
	}

	void InstantiateCurrentDemon() {
		_currentDemonInstance = Instantiate (_currentDemon.ArtAsset) as GameObject;

		// TODO: REPLACE WITH PREFAB CODE ONCE ASSETS ARE 3D
		_currentDemonInstance.transform.SetParent(GameObject.Find("DemonCanvas").transform);
		_currentDemonInstance.transform.Find ("Name").GetComponent<Text> ().text = _currentDemon.Name;
		_currentDemonInstance.transform.position = new Vector3 (50, 0, 0);
	}
}
