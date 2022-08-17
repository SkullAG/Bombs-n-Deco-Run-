using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAllFollowers : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		FlockManager flock = other.GetComponent<FlockManager>();
		if(flock)
		{
			for(int i = flock.flockMembers.Count - 1; i >= 0; i--)
            {
				flock.DefuseMember(flock.flockMembers[i]);
				//flock.flockMembers[i].autoKill();
            }
		}
	}
}
