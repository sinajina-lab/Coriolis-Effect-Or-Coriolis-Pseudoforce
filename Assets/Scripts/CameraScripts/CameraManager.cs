using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineFreeLook[] cameras;

    public CinemachineFreeLook FirstPersonCam;
    public CinemachineFreeLook ThirdPersonCam;
    public CinemachineFreeLook TopDownCam;
    public CinemachineFreeLook IsometricCam;

    public CinemachineFreeLook startCamera;
    public CinemachineFreeLook currentCam;

    // The currently active camera.
    private CinemachineFreeLook currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial camera to be the Third Person camera.
        currentCamera = ThirdPersonCam;

        // Set the priority of each camera based on the desired order.
        SetCameraPriorities();

        // Disable all cameras except the current one.
        SwitchCamera(currentCamera);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for user input to switch cameras.
        if (Input.GetMouseButtonDown(0))
        {
            // Tap detected, switch to the next camera.
            SwitchToNextCamera();
        }
    }

    // Switch to the specified camera.
    public void SwitchCamera(CinemachineFreeLook newCam)
    {
        currentCamera = newCam;

        // Update camera priorities.
        SetCameraPriorities();
    }

    // Switch to the next camera based on the desired priority order.
    private void SwitchToNextCamera()
    {
        if (currentCamera == FirstPersonCam)
        {
            SwitchCamera(ThirdPersonCam);
        }
        else if (currentCamera == ThirdPersonCam)
        {
            SwitchCamera(TopDownCam);
        }
        else if (currentCamera == TopDownCam)
        {
            SwitchCamera(IsometricCam);
        }
        else if (currentCamera == IsometricCam)
        {
            SwitchCamera(FirstPersonCam);
        }
    }

    // Set the camera priorities based on the desired order.
    private void SetCameraPriorities()
    {
        FirstPersonCam.m_Priority = (currentCamera == FirstPersonCam) ? 20 : 10;
        ThirdPersonCam.m_Priority = (currentCamera == ThirdPersonCam) ? 20 : 10;
        TopDownCam.m_Priority = (currentCamera == TopDownCam) ? 20 : 10;
        IsometricCam.m_Priority = (currentCamera == IsometricCam) ? 20 : 10;
    }
}
