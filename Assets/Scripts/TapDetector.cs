using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetector : MonoBehaviour
{
    public int tapTimes;
    public float resetTimer;
    public bool isHoldingDown;

    //[SerializeField] private InventorySlot IS;

    IEnumerator ResetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            StartCoroutine("ResetTapTimes");
            tapTimes++;
            //singletap
        }

        if (tapTimes >= 2)
        {
            tapTimes = 0;
            //doubletap

           
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            isHoldingDown = true;
            //holddown
            
        }
        else
            isHoldingDown = false;
            
    }
}
