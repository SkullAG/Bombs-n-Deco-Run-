using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMultiplier : MonoBehaviour
{
    public int multiplier = 5;
    public int timeInSeconds = 3;
    private void OnTriggerEnter(Collider other)
    {
        FlockManager flock = other.GetComponent<FlockManager>();
        PlayerController player = other.GetComponent<PlayerController>();
        if (player && flock)
        {
            StartCoroutine(player.SpeedUp(multiplier, timeInSeconds));
            flock.SpeedUp(multiplier, timeInSeconds);
        }
    }
}
