using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    protected void OnCollisionStay(Collision collision)
    {
        AffectHazard(collision.collider);
    }

    protected void OnTriggerStay(Collider other)
    {
        AffectHazard(other);
    }

    public abstract void AffectHazard(Collider other);
}
