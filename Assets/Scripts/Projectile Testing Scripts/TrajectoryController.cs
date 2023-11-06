using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    public LineRenderer trajectoryLine;
    public Transform cannonTransform;
    public GameObject cannonballPrefab;
    public Transform spawnPoint;
    public LayerMask collidableLayer;
    public float initialVelocity = 10.0f;
    public float updateRate = 0.1f; // Rate of updating the trajectory

    private void Start()
    {
        trajectoryLine.positionCount = 0; // Initialize with no points
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateTrajectory();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LaunchCannonball();
        }
    }

    private void UpdateTrajectory()
    {
        Vector3 startPos = cannonTransform.position;
        Vector3 direction = cannonTransform.forward; // Adapt to cannon direction
        Vector3 velocity = direction * initialVelocity; // Adjust this value
        float gravity = Physics.gravity.y;

        // Calculate the trajectory points
        Vector3[] points = CalculateTrajectoryPoints(startPos, velocity, gravity);

        // Update the line renderer
        trajectoryLine.positionCount = points.Length;
        trajectoryLine.SetPositions(points);
    }

    private void LaunchCannonball()
    {
        Vector3 startPos = spawnPoint.position;
        Vector3 direction = cannonTransform.forward;
        Vector3 velocity = direction * initialVelocity;

        GameObject cannonball = Instantiate(cannonballPrefab, startPos, Quaternion.identity);
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        rb.velocity = velocity;
        Destroy(cannonball, 5.0f); // Destroy after 5 seconds

        trajectoryLine.positionCount = 0; // Clear the trajectory after launch
    }

    private Vector3[] CalculateTrajectoryPoints(Vector3 startPos, Vector3 velocity, float gravity)
    {
        // Implement your trajectory calculation logic here
        // You can use a loop to calculate points over time
        // Example: Calculate points using the kinematic equations
        // Return an array of points
        List<Vector3> points = new List<Vector3>();
        float timeStep = updateRate;
        Vector3 currentPos = startPos;
        Vector3 currentVelocity = velocity;

        for (float t = 0; t < 10.0f; t += timeStep)
        {
            currentVelocity += new Vector3(0, gravity * timeStep, 0);
            currentPos += currentVelocity * timeStep;
            points.Add(currentPos);

            // Check for collisions with collidable objects
            RaycastHit hit;
            if (Physics.Raycast(currentPos, currentVelocity.normalized, out hit, currentVelocity.magnitude * timeStep, collidableLayer))
            {
                trajectoryLine.positionCount = points.Count;
                break;
            }
        }

        return points.ToArray();
    }
}