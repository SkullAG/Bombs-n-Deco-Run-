using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RulerBehabiour : MonoBehaviour
{
#if UNITY_EDITOR
	public Transform UpPoint;
	public Transform BottomPoint;

	Mesh Rmesh;

	public float RulerLength;

	private void Start()
	{
		Rmesh = Measurement.GetQuad();
	}

	private void OnDrawGizmos()
	{
		RulerLength = Vector3.Distance(UpPoint.position, BottomPoint.position);

		Vector3 Scale = new Vector3(1, 1, RulerLength);

		BottomPoint.up = (UpPoint.position - BottomPoint.position).normalized;//Camera.current.transform.rotation.eulerAngles.y
		UpPoint.up = BottomPoint.up;

		Vector3 cameraInRulerPosition = BottomPoint.InverseTransformPoint(Camera.current.transform.position);

		Vector2 lookDir = new Vector2(BottomPoint.localPosition.x - cameraInRulerPosition.x, BottomPoint.localPosition.z - cameraInRulerPosition.z);

		Quaternion rotation = BottomPoint.rotation * Quaternion.Euler(0, -Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90, 0);

		Gizmos.color = Color.yellow;
		
		Gizmos.DrawMesh(Rmesh, BottomPoint.position, rotation, new Vector3(.5f, Vector3.Distance(UpPoint.position, BottomPoint.position), 1));

		Gizmos.DrawIcon(UpPoint.position, "sv_icon_dot13_pix16_gizmo", true);

		//Handles.DrawGizmos(Camera.current);

		//Handles.Label(UpPoint.position, rotation.eulerAngles.x + " m");
	}
#endif
}
