using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BringToFrontOnClick : MonoBehaviour, IPointerClickHandler {
	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		GetComponent<Canvas> ().sortingLayerName = "UIFront";
	}

	#endregion


}
