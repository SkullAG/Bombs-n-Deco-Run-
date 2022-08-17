using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GodMode : MonoBehaviour
{
    public HealthSistem Health;

    public FlockManager flock;

    public Vector3 TpPosition = new Vector3(0, 0, 390);

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

    public void TeleportToEnd(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            flock.transform.position = TpPosition;

            for (int i = 0; i < flock.flockMembers.Count; i++)
            {
                flock.flockMembers[i].transform.position = TpPosition + new Vector3(0, i, -2);
            }
        }
    }
}
