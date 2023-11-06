using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    [Header("Managers")]
    public CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            cameraManager.SwitchCamera(cameraManager.FirstPersonCam);
        }

        if (Input.GetMouseButton(0))
        {
            cameraManager.SwitchCamera(cameraManager.ThirdPersonCam);
        }

        if (Input.GetMouseButton(0))
        {
            cameraManager.SwitchCamera(cameraManager.TopDownCam);
        }

        if (Input.GetMouseButton(0))
        {
            cameraManager.SwitchCamera(cameraManager.IsometricCam);
        }
    }
}
