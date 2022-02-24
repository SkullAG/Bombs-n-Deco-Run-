using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CameraManager : MonoBehaviour
{
	public Transform objective;
	public float acceleration;
	public Vector3 offset = Vector3.zero;
	Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		//offset = transform.position - objective.position;
	}

	private void FixedUpdate()
	{
		_rb.MovePosition(Vector3.Lerp(transform.position, objective.position + offset, acceleration * Time.deltaTime));
	}
}
