using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchShot : MonoBehaviour
{
    [SerializeField] float _InitialVelocity;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] int _numberOfTrajectoryPoints = 50;

    [SerializeField] Transform _firePoint;
    [SerializeField] Transform _cannon;

    public GameObject cannonballPrefab;
    public Transform spawnPoint;
    //public float cannonballSpeed = 10f;
    public float trajectoryDistanceMultiplier = 1.0f;

    private void Update()
    {
        // Calculate the launch angle based on the orientation of the cannon
        float angle = CalculateAngle();
        // Calculate the trajectory based on the launch angle
        CalculateTrajectory(angle);

        if (Input.GetMouseButtonUp(0))
        {
            FireCannonball(angle);
        }
    }

    private float CalculateAngle()
    {
        // Calculate the angle between the cannon's forward vector and the positive x-axis
        //float angle = Vector3.SignedAngle(_cannon.forward, Vector3.right, Vector3.up);
        float angle = Vector3.SignedAngle(_cannon.up, Vector3.up, _cannon.right);
        return angle * Mathf.Deg2Rad;
    }

    private void CalculateTrajectory(float angle)
    {
        float v0 = _InitialVelocity;
        float g = Physics.gravity.y;
        float stepSize = CalculateTrajectoryDistance(angle) / _numberOfTrajectoryPoints;

        //Vector3 startPoint = _firePoint.position;
        Vector3[] localTrajectoryPoints = new Vector3[_numberOfTrajectoryPoints];

        for (int i = 0; i < _numberOfTrajectoryPoints; i++)
        {
            float t = i * stepSize;
            float x = t;
            float y = x * Mathf.Tan(angle) - (g * x * x) / (2 * v0 * v0 * Mathf.Cos(angle) * Mathf.Cos(angle));
            localTrajectoryPoints[i] = new Vector3(x, y, 0);
        }

        // Transform local space to global space without inversion
        Matrix4x4 localToGlobal = _firePoint.localToWorldMatrix;

        Vector3[] globalTrajectoryPoints = new Vector3[_numberOfTrajectoryPoints];
        for (int i = 0; i < _numberOfTrajectoryPoints; i++)
        {
            globalTrajectoryPoints[i] = localToGlobal.MultiplyPoint3x4(localTrajectoryPoints[i]);
        }

        _lineRenderer.positionCount = _numberOfTrajectoryPoints;
        _lineRenderer.SetPositions(globalTrajectoryPoints);
    }

    private float CalculateTrajectoryDistance(float angle)
    {
        return trajectoryDistanceMultiplier * _InitialVelocity * Mathf.Cos(angle) / Physics.gravity.magnitude;
    }

    private void FireCannonball(float angle)
    {
        GameObject cannonball = Instantiate(cannonballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 launchDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            //rb.velocity = launchDirection * cannonballSpeed;
            rb.velocity = launchDirection * _InitialVelocity;
        }

        Destroy(cannonball, 2f);
    }
}
