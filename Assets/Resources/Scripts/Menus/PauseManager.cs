using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
	public static bool IsPaused { get; private set; }

	public static void PauseGame()
	{
		Debug.Log("Game Paused");
		Time.timeScale = 0;
		IsPaused = true;
	}

	public static void RessumeGame()
	{
		Debug.Log("Game Resumed");
		Time.timeScale = 1;
		IsPaused = false;
	}

	public static void SetPauseState(bool state)
	{
		if(state)
        {
			PauseGame();
		}
		else
        {
			RessumeGame();
		}
	}
}
