using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DemonDisplayManager : MonoBehaviour {
	private List<DemonData> _demons;
	private DemonData _currentDemon;
	private GameObject _currentDemonInstance;

	public Transform DemonContainer;

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
		_currentDemonInstance.transform.localScale = new Vector3 (1.48f, 1.48f, 1.48f);
		_currentDemonInstance.transform.position = new Vector3 (0, 0.2f, 0);
		_currentDemonInstance.transform.SetParent (DemonContainer, true);

		Transform canvas = GameObject.Find ("DemonCanvas").transform;
		canvas.Find("NameText").GetComponent<Text>().text = _currentDemon.Name;
		canvas.Find("ChantText").GetComponent<Text>().text = _currentDemon.Chant;
	}
}
