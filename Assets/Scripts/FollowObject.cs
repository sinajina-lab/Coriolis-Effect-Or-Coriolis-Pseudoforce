using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // The target to follow

    public float degrees;

    private void Update()
    {
        
    }
    private void Start()
    {
        transform.RotateAround(target.position, transform.up, degrees * Time.deltaTime);

        /*Vector3 targetVector = target.forward * 20;
        Vector3 offset = Quaternion.Euler(0, 30, 0) * targetVector;
        transform.position = target.position + offset; */
    }
}
