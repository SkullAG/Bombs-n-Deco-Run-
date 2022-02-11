using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMember : MonoBehaviour
{
	FlockManager flock;

	List<float> jumpPoints = new List<float>();

	public float velocity;
	public float acceleration;

	public void storeJumpOrder(float zDistance)
	{
		jumpPoints.Add(zDistance);
	}


	private void FixedUpdate()
	{
		if (!flock) return;


	}
}
