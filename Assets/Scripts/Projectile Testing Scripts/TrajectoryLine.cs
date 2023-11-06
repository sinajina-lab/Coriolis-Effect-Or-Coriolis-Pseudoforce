using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    // Reference to the CannonController script
    [SerializeField] CannonController cannonController;

    [SerializeField] GameObject cannon;

    // Reference to the cannonball GameObject
    [SerializeField] GameObject cannonball;

    //[SerializeField] Animator animator;
    //[SerializeField] Camera Camera;
    Rigidbody cannonballRB;

    [SerializeField] float ThrowStrength = 50f;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform releasePosition;

    [Header("Display Controls")]
    [Range(10, 100)]
    [SerializeField] int LinePoints = 25;

    internal void ShowTrajectoryLine(Vector3 position, Vector3 vector3)
    {
        throw new NotImplementedException();
    }

    [Range(0.01f, 0.25f)]
    [SerializeField] float timeBtwnPoints = 0.1f;

    [Range(1, 10)]
    [SerializeField] float ExplosionDelay = 5f;
    [SerializeField] GameObject ExplosionParticleSystem;

    [Header("Display Controls")]
    [SerializeField] Transform InitialParent;
    [SerializeField] Vector3 InitialLocalPosition;
    [SerializeField] Quaternion InitialRotation;

    [SerializeField] bool isCannonballAvailable = true;

    //The physics layer that will cause the line to stop being drawn
    public LayerMask Collidablelayers;

    private void Awake()
    {
        cannonballRB = cannonball.GetComponent<Rigidbody>();

        InitialRotation = cannonballRB.transform.localRotation;
        InitialLocalPosition = cannonballRB.transform.localPosition;
        cannonballRB.freezeRotation = true;

        // Set the layer number for the cannonball to a valid value
        int validLayerNumber = Mathf.Clamp(cannonballRB.gameObject.layer, 0, 31);
        cannonballRB.gameObject.layer = validLayerNumber;

        int CollidableLayer = cannonballRB.gameObject.layer;
        for (int i = 0; i < 31; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(CollidableLayer, i))
            {
                CollidableLayer |= 1 << i;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cannonController != null)
        {
            // Adjust the LineRenderer's local position and rotation to match the CannonController
            lineRenderer.transform.localPosition = cannonController.transform.position;
            lineRenderer.transform.localRotation = cannonController.transform.rotation;
        }
        else
        {
            Debug.LogError("CannonController reference not set in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isFocused && Input.GetMouseButton(0))
        {
            DrawProjection();

            if (Input.GetMouseButtonDown(0) && isCannonballAvailable)
            {
                isCannonballAvailable = false;
            }
        }

        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                RealeaseCannonBall();
                Debug.Log("Throw Cannonball");
            }
        }

    }
    void DrawProjection()
    {
        lineRenderer.enabled = true;

        Vector3 startPosition = releasePosition.position;
        Vector3 startVelocity = ThrowStrength * cannon.transform.forward / cannonballRB.mass; //Use the cannon's forward direction

        Vector3 currentPoint = startPosition;
        int i = 0;

        while (i < LinePoints)
        {
            lineRenderer.positionCount = i + 1;
            lineRenderer.SetPosition(i, currentPoint);

            //Vector3 lastPosition = lineRenderer.GetPosition(i - 1);
            Vector3 nextPoint = currentPoint + startVelocity * timeBtwnPoints;
            nextPoint.x = currentPoint.x + startVelocity.y * timeBtwnPoints + 0.5f * Physics.gravity.y * timeBtwnPoints * timeBtwnPoints;

            RaycastHit hit;
            if (Physics.Raycast(currentPoint, (nextPoint - currentPoint).normalized, out hit, (nextPoint - currentPoint).magnitude, Collidablelayers, QueryTriggerInteraction.Ignore))
            {
                lineRenderer.positionCount = i + 2;
                lineRenderer.SetPosition(i + 1, hit.point);
                break;
            }

            currentPoint = nextPoint;
            i++;
        }

        // Set the remaining points to the last calculated point to make the line renderer invisible
        for (int j = i + 1; j < LinePoints; j++)
        {
            lineRenderer.SetPosition(j, currentPoint);
        }
    }

    private void RealeaseCannonBall()
    {
        if (cannonball != null)
        {
            Rigidbody cannonballRB = cannonball.GetComponent<Rigidbody>();
            if (cannonballRB != null)
            {
                cannonballRB.velocity = Vector3.zero;
                cannonballRB.angularVelocity = Vector3.zero;
                cannonballRB.isKinematic = false;
                cannonballRB.freezeRotation = false;
                cannonball.transform.SetParent(null, true);
                cannonballRB.AddForce(cannon.transform.forward * ThrowStrength, ForceMode.Impulse);
               // StartCoroutine(ExplodeCannonball());
            }
            else
            {
                Debug.LogError("Rigidbody component not found on the cannonball.");
            }
        }
        else
        {
            Debug.LogError("Cannonball GameObject reference is not set in the Inspector.");
        }
    }
   /* private IEnumerator ExplodeCannonball()
    {
        yield return new WaitForSeconds(0f);

        Instantiate(ExplosionParticleSystem, cannonballRB.transform.position, Quaternion.identity);

        //cannonballRB.GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse(new Vector3(Random.Range(-1, 1), Random.Range(0.5f, 1), Random.Range(-1, 1)));

        cannonballRB.freezeRotation = true;
        cannonballRB.isKinematic = true;
        cannonballRB.transform.SetParent(InitialParent, false);
        cannonballRB.rotation = InitialRotation;
        cannonballRB.transform.localPosition = InitialLocalPosition;
        isCannonballAvailable = true;
    }*/
}
