using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class FallingDeath : MonoBehaviour
{	
	HealthSistem hp;
	Rigidbody _rb;
	Collider[] colliders;

	public float upLaunchVelocity = 10;
	public float Fallingseconds = 3;

	private void Awake()
	{
		hp = GetComponent<HealthSistem>();
		colliders = GetComponentsInChildren<Collider>();
		_rb = GetComponent<Rigidbody>();
	}
	void OnEnable()
	{
		hp.OnDead.AddListener(Die);
	}

	void OnDisable()
	{
		hp.OnDead.RemoveListener(Die);
	}

	// Update is called once per frame
	void Die()
	{
		foreach (Collider c in colliders)
		{
			c.enabled = false;
		}

		_rb.velocity = (Vector3.up * upLaunchVelocity) + (Random.Range(-1f, 1f) * Vector3.one);

		_rb.angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));

		StartCoroutine(Timer());
		
	}

    IEnumerator Timer()
    {
		yield return new WaitForSeconds(Fallingseconds);
		this.gameObject.SetActive(false);
    }
}
