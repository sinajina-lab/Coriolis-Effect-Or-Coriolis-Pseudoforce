using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLaunch : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField, Min(3)] int lineSegments = 60;

    [SerializeField, Min(1)] float timeOfTheFlight = 5;

    //The physics layer that will cause the line to stop being drawn
    public LayerMask Collidablelayers;

    public void ShowTrajectoryLine(Vector3 startpoint, Vector3 startVelocity)
    {
        //The more points we add the smoother the line will be
        float timeStep = timeOfTheFlight / lineSegments;

        Vector3[] lineRendererPoints = CalculateTrajectoryLine(startpoint, startVelocity, timeStep);

        lineRenderer.positionCount = lineSegments;
        lineRenderer.SetPositions(lineRendererPoints);
    }
    private Vector3[] CalculateTrajectoryLine(Vector3 startpoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[lineSegments];

        lineRendererPoints[0] = startpoint;

        for(int i = 1; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = startpoint + progressBeforeGravity - gravityOffset;
            lineRendererPoints[i] = newPosition;

            // Check for collisions with collidable objects
            if (Physics.Raycast(lineRendererPoints[i - 1], lineRendererPoints[i] - lineRendererPoints[i - 1], out RaycastHit hit, Vector3.Distance(lineRendererPoints[i - 1], lineRendererPoints[i]), Collidablelayers))
            {
                // Handle the collision as needed
                // For example, you can break the loop or update the position based on the collision point
                lineRendererPoints[i] = hit.point;
                break;
            }
        }

        return lineRendererPoints;
    }
}
