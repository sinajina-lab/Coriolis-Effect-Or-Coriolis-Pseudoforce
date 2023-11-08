using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    [SerializeField] List<Iam_InTheWay> currentlyInTheWay;
    [SerializeField] List<Iam_InTheWay> alreadyTransparent;
    [SerializeField] Transform player;
    Transform _camera;

    private void Awake()
    {
        currentlyInTheWay = new List<Iam_InTheWay>();
        alreadyTransparent = new List<Iam_InTheWay>();

        _camera = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetAllObjectsInTheWay();

        MakeObjectsSolid();
        MakeObjectsTransparent();
    }

    void GetAllObjectsInTheWay()
    {
        currentlyInTheWay.Clear();

        float cameraPlayerDistance = Vector3.Magnitude(_camera.position - player.position);

        Ray ray1_Forward = new Ray(_camera.position, player.position - _camera.position);
        Ray ray1_Backwards = new Ray(player.position, _camera.position - player.position);

        var hits1_Forward = Physics.RaycastAll(ray1_Forward, cameraPlayerDistance);
        var hits1_Backward = Physics.RaycastAll(ray1_Backwards, cameraPlayerDistance);

        foreach(var hit in hits1_Forward)
        {
            if(hit.collider.gameObject.TryGetComponent(out Iam_InTheWay inTheWay))
            {
                if(!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
        }

        foreach (var hit in hits1_Backward)
        {
            if (hit.collider.gameObject.TryGetComponent(out Iam_InTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
        }
    }

    void MakeObjectsTransparent()
    {
        for(int i = 0; i < currentlyInTheWay.Count; i++)
        {
            Iam_InTheWay inTheWay = currentlyInTheWay[i];

            if(!alreadyTransparent.Contains(inTheWay))
            {
                inTheWay.ShowTransparent();
                alreadyTransparent.Add(inTheWay);
            }
        }
    }

    void MakeObjectsSolid()
    { 
        for(int i = alreadyTransparent.Count - 1; i >= 0; i--)
        {
            Iam_InTheWay wasInTheWay = alreadyTransparent[i];

            if(!currentlyInTheWay.Contains(wasInTheWay))
            {
                wasInTheWay.ShowSolid();
                alreadyTransparent.Remove(wasInTheWay);
            }
        }
    }

    
}
