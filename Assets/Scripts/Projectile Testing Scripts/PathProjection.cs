using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathProjection : MonoBehaviour
{
    // Reference to the CannonController script
    [SerializeField] CannonController cannonController;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform firePosition;
    [SerializeField] float firePower = 20;
    float initialAngle = 30;

    [Header("Display Controls")]
    [Range(0, 100)]
    [SerializeField] int i = 0;
    [SerializeField] int NumberOfPoints = 20;

    //The physics layer that will cause the line to stop being drawn
    [SerializeField] LayerMask Collidablelayers;

    float timer = 0.1f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Application.isFocused && Input.GetMouseButton(0))
        {
            DrawProjection();
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                FireCannonBall();
                Debug.Log("Throw Cannonball");
            }
        }
    }
    void DrawProjection()
    {
        i = 0;
        lineRenderer.positionCount = NumberOfPoints;
        lineRenderer.enabled = true;

        Vector3 startVelocity = cannonController.transform.forward * firePower / rb.mass; //Use the cannon's forward direction
        Vector3 currentPosition = firePosition.position; // Set the starting position to the firePosition

        for (float j = 0; j < lineRenderer.positionCount; j += timer)
        {
            i++;
            Vector3 linePosition = currentPosition + j * startVelocity;
            linePosition.y = currentPosition.y + startVelocity.y * j + 0.5f * Physics.gravity.y * j * j;
            
            //Use Raycast to check for collisions with collidable layers
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, linePosition - currentPosition, out hit, (linePosition - currentPosition).magnitude, Collidablelayers))
            {
                //Adjust the line position to the collision point
                linePosition = hit.point;
                lineRenderer.positionCount = i; //Shorten the line to the collision point.
                break;
            }

            lineRenderer.SetPosition(i, linePosition);
        }
    }
    void FireCannonBall()
    {
        rb.AddForce(cannonController.transform.forward * firePower);
    }
}
