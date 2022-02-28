using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GodMode : MonoBehaviour
{
    public HealthSistem Health;

    public FlockManager flock;

    public void SetGodModeState(bool state)
    {
        Health.MaxHealth = Mathf.Infinity;
        Health.health = Mathf.Infinity;

        Debug.Log("God Mode On");
    }

    public void AddFollower(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            flock.AddFollower();
            Debug.Log("Follower Added");
        }
    }
}
