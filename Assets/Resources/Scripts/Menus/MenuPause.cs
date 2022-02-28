using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MenuPause : MonoBehaviour
{
	public void Activate(bool state)
	{
		gameObject.SetActive(state);

		PauseManager.SetPauseState(state);
	}

	public void AlternateActivation()
	{

		//Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA");
		Activate(!gameObject.activeInHierarchy);
	}
}
