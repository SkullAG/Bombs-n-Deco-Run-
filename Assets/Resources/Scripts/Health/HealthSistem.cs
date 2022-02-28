using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthSistem : MonoBehaviour
{
	public float health = 100;
	public float MaxHealth = 100;

	public float inmunityTime = 0;

	private float actualInmunityTime = 0;

	public UnityEvent OnDead;

	public AudioClip HurtSound;
	//[Serializable]public class OnLife : UnityEvent<int> {}

	public UnityEvent<float> OnLifeChange;
	public UnityEvent<float> OnMaxLifeChange;

    private void FixedUpdate()
    {
		if (actualInmunityTime > 0)
			actualInmunityTime -= Time.deltaTime;

	}

	public void HurtWithoutInmunity(float damage)
    {
		Hurt(damage, true);

	}

	public void HurtWithInmunity(float damage)
	{
		Hurt(damage, false);

	}

	public void Hurt(float damage, bool ignoreInmunityTime = false)
	{
		//Debug.Log(damage);
		if(actualInmunityTime <= 0 || ignoreInmunityTime)
        {
			if(HurtSound)
            {
				SoundDelegation.PlaySoundEffect(HurtSound);
            }

			if(!ignoreInmunityTime)
            {
				actualInmunityTime = inmunityTime;
            }
			

			health -= damage;

			if (health > MaxHealth)
			{
				health = MaxHealth;
			}
			else if (health < 0)
			{
				health = 0;
			}
			
			if(health != Mathf.Infinity)
			OnLifeChange.Invoke(health);


			if (health<=0)
			{
				OnDead.Invoke();
			}
        }
		
	}
}
