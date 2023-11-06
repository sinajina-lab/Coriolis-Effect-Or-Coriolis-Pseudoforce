using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLineRenderer : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    public Material lineMaterial;
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public float lineLength = 100f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
    }

    public void SetPositions(Vector3[] newPositions)
    {
        positions.Clear();
        positions.AddRange(newPositions);

        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
