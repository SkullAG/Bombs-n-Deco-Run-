using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunOnProximity : MonoBehaviour
{
	public Transform objective;
	Rigidbody _rb;
	HealthSistem hp;

	public float distance;

	public Vector3 velocity;

    private void Start()
    {
		_rb = GetComponent<Rigidbody>();
		hp = GetComponent<HealthSistem>();
	}

    // Update is called once per frame
    void Update()
	{
		if(Vector3.Distance(objective.position, transform.position) <= distance && (!hp || hp.health > 0))
		{
			_rb.velocity = velocity;
		}
	}
}
