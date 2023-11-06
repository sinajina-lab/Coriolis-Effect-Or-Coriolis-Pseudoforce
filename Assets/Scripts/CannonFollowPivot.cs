using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFollowPivot : MonoBehaviour
{
    [SerializeField] GameObject pivot;
    [SerializeField] GameObject cannon;

    private Quaternion initialRotation;

    private void Awake()
    {
        // Store the initial local rotation of the cannon
        initialRotation = cannon.transform.localRotation;

        // Detach the cannon from its current parent
        cannon.transform.SetParent(null);

        // Reparent the cannon to the pivot
        cannon.transform.SetParent(pivot.transform);

        // Reset the local rotation of the cannon
        cannon.transform.localRotation = initialRotation;
    }
}
