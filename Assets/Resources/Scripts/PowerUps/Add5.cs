using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add5 : MonoBehaviour
{
    public int membersToAdd = 5;
    private void OnTriggerEnter(Collider other)
    {
        FlockManager flock = other.GetComponent<FlockManager>();
        if(flock)
        {
            for(int i = 0; i < membersToAdd; i++)
            {
                flock.AddFollower();
            }
        }
    }
}
