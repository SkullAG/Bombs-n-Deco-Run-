using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class DrawTowerDisplacement : MonoBehaviour
{
	[System.Serializable]
	public struct DrawingObjects
	{
		public Mesh mesh;
		public Material material;
		public Vector3 position;
		public Vector3 rotation;
		public Vector3 scale;
		public List<List<Matrix4x4>> matrix;
	}

	[SerializeField]
	public DrawingObjects[] objectsToDraw;

	public Vector2 size = Vector2.one;

	public Vector3 Density = Vector3.one;

	public int minHeight = 1;
	public int maxHeight = 1;

	public Vector3 objectRotation;

	public bool Redo = true;

	Camera cam;

	//public float randomPositionMultiplier;
	//public float randomRotationMultiplier;
	private void Awake()
	{
		Recalculate();

		cam = Camera.main;
	}

	public void Recalculate()
	{
		if(Density.x <= 0 || Density.y <= 0 || Density.z <= 0)
        {
			return;
        }

		for (int i = 0; i < objectsToDraw.Length; i++)
		{
			objectsToDraw[i].matrix = new List<List<Matrix4x4>>();
			objectsToDraw[i].matrix.Clear();
		}

		for (float x = 0; x <= size.x; x += 1f / Density.x)
		{
			for (float y = 0; y <= size.y; y += 1f / Density.z)
			{
				int height = Random.Range(minHeight, maxHeight + 1);
				for (float h = 0; h <= height; h++)
				{
					int objIndex = Random.Range(0, maxExclusive: objectsToDraw.Length);

					Matrix4x4 matrix = Matrix4x4.Translate(transform.position) *
						(Matrix4x4.Rotate(transform.rotation) * Matrix4x4.Translate(objectsToDraw[objIndex].position + new Vector3(x - size.x / 2, h * (1f / Density.y), y - size.y / 2) /*+ new Vector3(Random.value, 0, Random.value) * randomPositionMultiplier*/)) *
						Matrix4x4.Rotate(Quaternion.Euler(objectRotation + objectsToDraw[objIndex].rotation/* + Random.insideUnitSphere * randomRotationMultiplier*/)) *
						Matrix4x4.Scale(objectsToDraw[objIndex].scale);

					if (objectsToDraw[objIndex].matrix.Count == 0)
					{
						objectsToDraw[objIndex].matrix.Add(new List<Matrix4x4>());
					}
					if (objectsToDraw[objIndex].matrix[objectsToDraw[objIndex].matrix.Count - 1].Count >= 1000)
					{
						objectsToDraw[objIndex].matrix.Add(new List<Matrix4x4>());
					}

					objectsToDraw[objIndex].matrix[objectsToDraw[objIndex].matrix.Count - 1].Add(matrix);
					//Debug.Log("a");
				}
			}
		}
	}

	void Update()
	{
#if UNITY_EDITOR
		if (Redo && !Application.isPlaying) Recalculate();

		if (minHeight > maxHeight)
		{
			minHeight = maxHeight;

		}
#endif
		for (int i = 0; i < objectsToDraw.Length; i++)
		{
			for (int e = 0; e < objectsToDraw[i].matrix.Count; e++)
			{
				Graphics.DrawMeshInstanced(objectsToDraw[i].mesh, 0, objectsToDraw[i].material, objectsToDraw[i].matrix[e]);
			}
		}
	}
}
