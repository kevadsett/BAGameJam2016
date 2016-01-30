using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayOpenAnimOnClick : MonoBehaviour, IPointerClickHandler {
	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		Animator animator = GetComponent<Animator> ();
		if (animator) {
			animator.SetBool ("IsOpen", true);
		}
	}

	#endregion


}
