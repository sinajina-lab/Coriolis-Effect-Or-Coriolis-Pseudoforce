using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajecory : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;

    [Range(3, 30)]
    [SerializeField] int _lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();

    #region Singleton

    public static DrawTrajecory Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;

        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        float stepTime = FlightDuration / _lineSegmentCount;

        _linePoints.Clear();
        _linePoints.Add(startingPoint);

        for(int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; //change in time

            Vector3 MovementVector = new Vector3(
                x: velocity.x * stepTimePassed,
                y: velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                z: velocity.z * stepTimePassed);

            _linePoints.Add(item: -MovementVector + startingPoint);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }
    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
