using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iam_InTheWay : MonoBehaviour
{
    [SerializeField] GameObject solidBody;
    [SerializeField] GameObject transparentBody;

    private void Awake()
    {
        ShowSolid();
    }
    public void ShowTransparent()
    {
        solidBody.SetActive(false);
        transparentBody.SetActive(true);
    }
    public void ShowSolid()
    {
        solidBody.SetActive(true);
        transparentBody.SetActive(false);
    }
}
