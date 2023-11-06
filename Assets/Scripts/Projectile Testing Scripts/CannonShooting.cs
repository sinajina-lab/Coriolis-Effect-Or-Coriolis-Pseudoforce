using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] TrajectoryLine trajectoryLine;

    [SerializeField] GameObject cannonBall;
    [SerializeField] Transform muzzle;

    [SerializeField, Min(1)] float cannonBallMass = 30;

    [SerializeField, Min(1)] float shotForce = 30;

    [SerializeField] float shootingDelay = 1;
    [SerializeField] bool isWaiting = false;

    public UnityEvent OnShoot;

    public void Update()
    {
        if (Input.GetMouseButton(0) && isWaiting == false)
        {
            trajectoryLine.ShowTrajectoryLine(muzzle.position, muzzle.forward * shotForce / cannonBallMass);
            trajectoryLine.enabled = true;
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                trajectoryLine.enabled = false;

                GameObject ball = Instantiate(cannonBall);
                ball.transform.position = muzzle.position;

                OnShoot?.Invoke();

                Rigidbody rb = ball.GetComponent<Rigidbody>();
                rb.mass = cannonBallMass;
                rb.AddForce(muzzle.forward * shotForce, ForceMode.Impulse);

                isWaiting = true;
                StartCoroutine(DelayShooting());
            }
        }
    }
    private IEnumerator DelayShooting()
    {
        yield return new WaitForSeconds(shootingDelay);
        isWaiting = false;
    }
}
