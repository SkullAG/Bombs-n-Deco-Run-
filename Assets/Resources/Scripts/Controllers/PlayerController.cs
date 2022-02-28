using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float forwardVelocity;
	public float forwardAcceleration;

	public float lateralVelocity;
	public float lateralAcceleration;

	public float jumpForce;

	private PlayerInput _input;
	private Rigidbody _rb;
	private Animator _animator;

	private InputAction moveAction;

	public float groundCheckDistance;
	public LayerMask groundMask;

	public ParticleSystem doubleJumpEffect;

	bool isGrounded;

	bool hasSkyJump;
	bool hasStomp;

	void Start()
	{
		_input = GetComponent<PlayerInput>();
		_rb = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();

		moveAction = _input.actions.FindAction("Move");
	}

	public void jump(InputAction.CallbackContext context)
	{
		if(context.started && !PauseManager.IsPaused)
        {
			if (isGrounded)
			{
				_rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
			}
			else if(hasSkyJump)
			{
				_rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
				hasSkyJump = false;
				doubleJumpEffect.Play();
			}
        }
		
	}
	public void stomp(InputAction.CallbackContext context)
	{
		if (!isGrounded && hasStomp && context.started && !PauseManager.IsPaused)
		{
			_rb.velocity = new Vector3(_rb.velocity.x, -jumpForce, _rb.velocity.z);
			hasSkyJump = false;
			hasStomp = false;
			doubleJumpEffect.Play();
		}
	}

	void checkForGround()
	{
		isGrounded = Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundMask);

		_animator.SetBool("IsGrounded", isGrounded);
	}

	void CalculateMovement()
	{
		Vector3 vel = _rb.velocity;

		float xMovement = moveAction.ReadValue<Vector2>().x;

		float xVel = Utility.CalculateVelocity(vel.x, lateralAcceleration, lateralVelocity * xMovement);
		//float xVel = Mathf.Lerp(vel.x, lateralVelocity * xMovement, Mathf.Min((lateralAcceleration * Time.deltaTime) / Mathf.Abs(vel.x - lateralVelocity * xMovement), 1));

		float yVel = vel.y;

		float zVel = Utility.CalculateVelocity(vel.z, forwardAcceleration, forwardVelocity);
		//float zVel = Mathf.Clamp(vel.z + (forwardAcceleration * Time.deltaTime), 0, forwardVelocity);

		vel = new Vector3(xVel, yVel, zVel);

		_rb.velocity = vel;
	}
	
	void FixedUpdate()
	{
		checkForGround();
		if(isGrounded)
        {
			hasSkyJump = true;
			hasStomp = true;

		}

		CalculateMovement();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = isGrounded ? Color.green : Color.red;
		Gizmos.DrawLine(transform.position, transform.position - transform.up * groundCheckDistance);
	}
}
