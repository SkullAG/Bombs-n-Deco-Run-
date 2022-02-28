using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SimpleMenuPause : MonoBehaviour
{
	public void Activate(bool state)
	{
		gameObject.SetActive(state);
	}

	public void AlternateActivation()
	{
		Activate(!gameObject.activeInHierarchy);
	}
}
