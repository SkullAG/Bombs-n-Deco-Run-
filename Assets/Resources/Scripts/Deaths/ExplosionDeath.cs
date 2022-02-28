using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDeath : MonoBehaviour
{
	public GameObject explosion;

	public LayerMask hurtingLayers;

	public float explosionRadius = 0.5f;

	public float explosionDamage = 10;

	public Vector3 explosionOffset = Vector3.zero;

	public AudioClip sound;

	HealthSistem hp;

	private void Awake()
	{
		hp = GetComponent<HealthSistem>();
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
		if(sound)
        {
			SoundDelegation.PlaySoundEffect(sound);
        }

		GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);

		exp.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);

		Collider[] cols = Physics.OverlapSphere(transform.position + explosionOffset, explosionRadius, hurtingLayers);

		//Debug.Log(cols.Length);

		foreach(Collider c in cols)
        {
			HealthSistem health = c.GetComponent<HealthSistem>();

			if(health)
			{
				health.Hurt(explosionDamage);
			}
        }

		//explosion.gameObject.transform.position = this.gameObject.transform.position;
		exp.GetComponent<ParticleSystem>().Play();
		this.gameObject.SetActive(false);
	}
}
