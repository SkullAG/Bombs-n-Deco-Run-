using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
	public List<FlockMember> flockMembers = new List<FlockMember>();
	public Transform Objective;

	public float MinDistanceToObjective;

	public float MinDistanceBetweenMembers;

	public void jumpOrder()
    {
		foreach(FlockMember f in flockMembers)
        {
			f.storeJumpOrder(Objective.position.z);
        }
    }
}
