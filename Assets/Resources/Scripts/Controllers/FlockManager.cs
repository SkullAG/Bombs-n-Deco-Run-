using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlockManager : MonoBehaviour
{
	public List<FlockMember> flockMembers = new List<FlockMember>();
	public Transform objective;

	public float MinDistanceToObjective;
	public float MaxDistanceToObjective = 50;

	public float MinDistanceBetweenMembers;

	public Transform AttackObjective;

	public Transform LaunchPoint;

	public Vector3 LaunchVelocity;

	Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();

		foreach (FlockMember f in flockMembers)
		{
			f.SetNewFlock(this);
		}
	}

	public void AddFollower()
	{
		FlockMember follower = PoolingManager.Instance.GetPooledObject("BombedLeg").GetComponent<FlockMember>();
		flockMembers.Add(follower);
		follower.SetNewFlock(this);
		follower.transform.position = LaunchPoint.position;
		follower.GetComponent<Rigidbody>().velocity = LaunchVelocity;
		follower.gameObject.SetActive(true);
	}

	public void jumpOrder(InputAction.CallbackContext context)
	{
		if (context.started && !PauseManager.IsPaused)
		{
			foreach (FlockMember f in flockMembers)
			{
				f.storeJumpOrder(objective.position.z);
			}
		}
	}

	public void stompOrder(InputAction.CallbackContext context)
	{
		if (context.started && !PauseManager.IsPaused)
		{
			foreach (FlockMember f in flockMembers)
			{
				f.storeStompOrder(objective.position.z);
			}
		}
	}

	public void attackOrder(InputAction.CallbackContext context)
	{
		if (context.started && !PauseManager.IsPaused)
		{
			foreach (FlockMember f in flockMembers)
			{
				if(!f.followingAttackOrder)
				{
					f.JumpTo(AttackObjective.position, _rb.velocity.z);
					return;
				}
			}
		}
	}

	public void SelfDestructOrder(InputAction.CallbackContext context)
	{
		if (context.started && !PauseManager.IsPaused && flockMembers.Count > 0)
		{
			flockMembers[0].SelfDefuse();
		}
	}

	private void Update()
    {
		foreach (FlockMember f in flockMembers)
		{
			if (Vector3.Distance(objective.position, f.transform.position) > MaxDistanceToObjective)
			{
				f.autoKill();
			}
		}
	}
}
