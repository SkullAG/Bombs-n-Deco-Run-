using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlockManager : MonoBehaviour
{
	public List<FlockMember> flockMembers = new List<FlockMember>();
	public Transform objective;

	public float MinDistanceToObjective;

	public float MinDistanceBetweenMembers;

	public Transform AttackObjective;

	Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();

		foreach (FlockMember f in flockMembers)
		{
			f.SetNewFlock(this);
		}
	}

	public void jumpOrder(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			foreach (FlockMember f in flockMembers)
			{
				f.storeJumpOrder(objective.position.z);
			}
		}
	}

	public void jumpOrder()
	{
		foreach (FlockMember f in flockMembers)
		{
			f.storeJumpOrder(objective.position.z);
		}
	}

	public void attackOrder(InputAction.CallbackContext context)
	{
		if (context.started)
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

	public void attackOrder()
	{
		foreach (FlockMember f in flockMembers)
		{
			if (!f.followingAttackOrder)
			{
				f.JumpTo(AttackObjective.position, _rb.velocity.z);
				return;
			}
		}
	}
}
