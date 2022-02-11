using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public static class Measurement
{
	static Mesh quadMesh;

	public static Mesh GetQuad()
    {
		if(!quadMesh)
        {
            quadMesh = new Mesh();

            Vector3[] vertices = new Vector3[4]
            {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0)
            };
            quadMesh.vertices = vertices;

            int[] tris = new int[6]
            {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
            };
            quadMesh.triangles = tris;

            Vector3[] normals = new Vector3[4]
            {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
            };
            quadMesh.normals = normals;

            Vector2[] uv = new Vector2[4]
            {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
            };
            quadMesh.uv = uv;
        }
		return quadMesh;
	}

	[MenuItem("GameObject/Measurements/SimpleRuler", false, 1)]
	public static void CreateRuler(MenuCommand menuCommand)
	{
		GameObject RulerContainer = new GameObject();
		RulerContainer.name = "SimpleRuler";

		RulerBehabiour RulerB = RulerContainer.AddComponent<RulerBehabiour>();

		GameObject RulerUpPoint = new GameObject();
		RulerUpPoint.name = "RulerUpPoint";

		RulerUpPoint.transform.parent = RulerContainer.transform;
		RulerUpPoint.transform.localPosition = Vector3.up;

		RulerB.UpPoint = RulerUpPoint.transform;

		GameObject RulerBottomPoint = new GameObject();
		RulerBottomPoint.name = "RulerBottomPoint";

		RulerBottomPoint.transform.parent = RulerContainer.transform;
		RulerBottomPoint.transform.localPosition = Vector3.zero;

		RulerB.BottomPoint = RulerBottomPoint.transform;

		GameObjectUtility.SetParentAndAlign(RulerContainer, menuCommand.context as GameObject);
	}
}
