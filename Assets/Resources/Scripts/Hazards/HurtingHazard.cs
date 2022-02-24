using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtingHazard : Hazard
{
    public float DamageDealt = 1;

    public override void AffectHazard(Collider other)
    {
        HealthSistem otherHP = other.GetComponent<HealthSistem>();

        if(otherHP)
        {
            otherHP.Hurt(DamageDealt);
            //Debug.Log("Burn!");
        }
    }
}
