using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlockManager))]
[ExecuteInEditMode]
public class AbsorbBomb : MonoBehaviour
{
	public Transform AbsorbPoint;
	public float AbsorbRadius;
	public float absorbDistance;
	public Vector3 absorbAngle;

	public LayerMask hitMask;

	RaycastHit hit;
	bool hitted;

	FlockManager flock;

	private void FixedUpdate()
	{
		hitted = Physics.SphereCast(AbsorbPoint.position, AbsorbRadius, Quaternion.Euler(absorbAngle) * AbsorbPoint.forward, out hit, absorbDistance, hitMask);

		flock = GetComponent<FlockManager>();

		if (hitted)
        {
			hit.collider.enabled = false;
			if(hit.rigidbody)
            {
				hit.rigidbody.useGravity = false;
            }


			StartCoroutine("Absorb", hit.transform);
		}
	}

	private void OnDrawGizmos()
	{
		Vector3 point = AbsorbPoint.position + (hitted ? hit.distance : absorbDistance) * (Quaternion.Euler(absorbAngle) * AbsorbPoint.forward);

		Gizmos.DrawLine(AbsorbPoint.position, point);
		Gizmos.DrawWireSphere(point, AbsorbRadius);
	}

	public IEnumerator Absorb(Transform obj)
    {
		float lerpVal = 0;
		Vector3 offset = AbsorbPoint.position - obj.position;


		while (lerpVal < 1)
        {
			lerpVal = lerpVal + Time.deltaTime*2;

            obj.position = AbsorbPoint.position - Vector3.Lerp(offset, Vector3.zero, lerpVal);

			yield return new WaitForFixedUpdate();
        }

		obj.gameObject.SetActive(false);
		flock.AddFollower();
	}
}
