using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningHazard : Hazard
{
    public float HeatIncreasePerSecond = 1;

    public override void AffectHazard(Collider other)
    {
        OverHeatingSystem otherHeat = other.GetComponent<OverHeatingSystem>();

        if (otherHeat)
        {
            otherHeat.Heat(HeatIncreasePerSecond * Time.deltaTime);
        }
    }
}
