using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    //[SerializeField] LineRenderer lineRenderer;

    //Cannon rotation variables
    [SerializeField] int speed = 8;
    [SerializeField] float friction = 1f;
    [SerializeField] float lerpSpeed = 5f;
    float xDegrees;
    float yDegrees;
    Quaternion fromRotation;
    Quaternion toRotation;

    private void Start()
    {
        /*if (lineRenderer != null)
        {
            // Set the LineRenderer's local rotation to face the -x axis
            lineRenderer.transform.localRotation = Quaternion.Euler(90, 90, 0);
            lineRenderer.transform.localPosition = transform.position;
        }
        else
        {
            Debug.LogError("LineRenderer reference not set in the Inspector.");
        }*/
    }
    private void Update()
    {
        if (Application.isFocused && Input.GetMouseButton(0))
        {
            xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
            yDegrees -= Input.GetAxis("Mouse X") * speed * friction;
            fromRotation = transform.rotation;
            toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        }
    }
}
