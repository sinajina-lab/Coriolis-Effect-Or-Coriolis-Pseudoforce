using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetector : MonoBehaviour
{
    public int tapTimes;
    public float resetTimer;
    public bool IsHoldingDown;

    // Start is called before the first frame update
    IEnumerator ResetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine("ResetTapTimes");
            tapTimes++;
        }

        if(tapTimes >= 2)
        {
            tapTimes = 0;
            //DoubleTap
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            IsHoldingDown = true;
            //HoldDown
        }
        else
        {
            IsHoldingDown = false;
        }
    }
}
