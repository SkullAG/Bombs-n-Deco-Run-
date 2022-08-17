using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMember : MonoBehaviour
{
	[SerializeField]
	private FlockManager flock;

	List<float> jumpPoints = new List<float>();
	List<float> stompPoints = new List<float>();

	public float velocity;
	private float calculatedVel;
	public float acceleration;

	public float jumpForce;

	Vector3 dir = Vector3.forward;

	private Rigidbody _rb;
	private Rigidbody objectiveRB;
	private Animator _animator;
	private HealthSistem _hp;

	public float groundCheckDistance;
	public LayerMask groundMask;
	bool isGrounded;

	Vector3 breakDistance = Vector3.zero;

	public int LayerWhenAttacking;
	int basicLayer;

	public bool followingAttackOrder { get; private set; }

	void checkForGround()
	{
		isGrounded = Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundMask);

		_animator.SetBool("IsGrounded", isGrounded);

		Debug.DrawLine(transform.position, transform.position - transform.up * groundCheckDistance, isGrounded ? Color.green : Color.red);
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();

		calculatedVel = velocity;

		basicLayer = gameObject.layer;

		_hp = GetComponent<HealthSistem>();

		if (flock)
		 objectiveRB = flock.objective.GetComponent<Rigidbody>();
	}

	public void storeJumpOrder(float zDistance)
	{
		jumpPoints.Add(zDistance);
	}
	public void storeStompOrder(float zDistance)
	{
		stompPoints.Add(zDistance);
	}

	private void FixedUpdate()
	{
		if(followingAttackOrder && isGrounded)
		{
			checkForGround();
			return;
		}

		checkForGround();

		if (followingAttackOrder && !isGrounded)
		{
			return;
			
		}
		else if(followingAttackOrder)
		{
			gameObject.layer = basicLayer;
			followingAttackOrder = false;
		}

		if (!flock) return;

		breakDistance.x = Mathf.Pow(_rb.velocity.x, 2) / acceleration;
		breakDistance.z = Mathf.Pow(_rb.velocity.z, 2) / acceleration;

		Vector3 vel = _rb.velocity;

		if (Mathf.Abs(flock.objective.position.x - transform.position.x) > (breakDistance.x > Mathf.Abs(objectiveRB.velocity.x) ? breakDistance.x - Mathf.Abs(objectiveRB.velocity.x) : 0))
		{
			dir.x = Mathf.Clamp(flock.objective.position.x - transform.position.x, -1, 1);
		}
		else
		{
			dir.x = 0;
		}

		if (Mathf.Abs(flock.objective.position.z - transform.position.z) > flock.MinDistanceToObjective + (breakDistance.z > Mathf.Abs(objectiveRB.velocity.z) ? breakDistance.z - Mathf.Abs(objectiveRB.velocity.z) : 0))
		{
			dir.z = Mathf.Clamp(flock.objective.position.z - transform.position.z, -1, 1);
		}
		else
		{
			dir.z = 0;
		}

		dir += avoidFlockMembersDir();

		float xVel = Utility.CalculateVelocity(vel.x, acceleration, calculatedVel * dir.x);

		float yVel = vel.y;

		float zVel = Utility.CalculateVelocity(vel.z, acceleration, calculatedVel * dir.z);

		vel = new Vector3(xVel, yVel, zVel);

		_rb.velocity = vel;

		for(int i = jumpPoints.Count-1; i >= 0; i--)
		{
			if(transform.position.z > jumpPoints[i] && transform.position.z < jumpPoints[i] + 1 && isGrounded)
			{
				_rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);

				jumpPoints.Remove(jumpPoints[i]);

				
			}
			else if(transform.position.z > jumpPoints[i] + 1)
			{
				jumpPoints.Remove(jumpPoints[i]);
			}
		}

		for (int i = stompPoints.Count - 1; i >= 0; i--)
		{
			if (transform.position.z > stompPoints[i] && transform.position.z < stompPoints[i] + 1 && !isGrounded)
			{
				_rb.velocity = new Vector3(_rb.velocity.x, -jumpForce, _rb.velocity.z);

				stompPoints.Remove(stompPoints[i]);


			}
			else if (transform.position.z > stompPoints[i] + 1)
			{
				stompPoints.Remove(stompPoints[i]);
			}
		}
		
	}

	Vector3 avoidFlockMembersDir()
	{
		Vector3 avoidDir = Vector3.zero;

		Vector3 TempDir = Vector3.zero;
		foreach (FlockMember f in flock.flockMembers)
		{
			//float breakDistance = Mathf.Pow(new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude, 2) / acceleration;
			if(f != this)
			{
				Vector3 vel = _rb.velocity;

				if (Mathf.Abs(f.transform.position.x - transform.position.x) < flock.MinDistanceBetweenMembers)
				{
					TempDir.x = -Mathf.Sign(f.transform.position.x - transform.position.x);
				}
				else
				{
					TempDir.x = 0;
				}

				if (Mathf.Abs(f.transform.position.z - transform.position.z) < flock.MinDistanceBetweenMembers)
				{
					TempDir.z = -Mathf.Sign(f.transform.position.z - transform.position.z);
				}
				else
				{
					TempDir.z = 0;
				}

				avoidDir += TempDir;
			}
		}

		avoidDir = avoidDir.normalized;
		//avoidDir = avoidDir / flock.flockMembers.Count;

		//Debug.Log(avoidDir);

		return avoidDir;

	}

	public void SetNewFlock(FlockManager f)
	{
		flock = f;
		objectiveRB = flock.objective.GetComponent<Rigidbody>();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = isGrounded ? Color.green : Color.red;
		Gizmos.DrawLine(transform.position, transform.position - transform.up * groundCheckDistance);
	}

	public void JumpTo(Vector3 point, float forwardVelocity = 0)
	{
		//float timeToFall = [jumpForce + Mathf.Sqrt(Mathf.Pow(jumpForce, 2) + 2 * Physics.gravity * h)] / Physics.gravity;

		float timeToFall = Mathf.Abs(2 * jumpForce / Physics.gravity.y);

		//Debug.Log(timeToFall);

		Vector3 dir = point - transform.position;

		//Debug.Log(dir);

		_rb.velocity = new Vector3(dir.x / timeToFall, jumpForce, dir.z / timeToFall + forwardVelocity);

		gameObject.layer = LayerWhenAttacking;


		followingAttackOrder = true;

	}

	public void detatchFromFlock()
	{
		if(flock)
		{
			flock.flockMembers.Remove(this);
		}
	}

	public void autoKill()
    {
		_hp.Hurt(Mathf.Infinity);

	}

	public void SelfDefuse()
	{
		if (flock)
		{
			flock.flockMembers.Remove(this);
		}

		this.gameObject.SetActive(false);
	}

	public IEnumerator SpeedUp(float mult, float time)
	{
		calculatedVel *= mult;
		_rb.velocity += new Vector3(0, 0, calculatedVel - _rb.velocity.z);
		yield return new WaitForSeconds(time);
		calculatedVel = velocity;

	}
}
