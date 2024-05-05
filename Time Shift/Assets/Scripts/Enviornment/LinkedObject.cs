using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedObject : MonoBehaviour
{
    // Info: Put this script on present objects that you want to be able to manipulate the place of future objects
    [Header("References")] 
    [SerializeField] private TimeShift timeShift;
    [SerializeField] private GameObject linkedFutureObj;
    private bool _positionUpdated = false;

    public void PositionHasUpdated()
    {
        _positionUpdated = true;
    }
    
    private void Start()
    {
        timeShift.OnTimeShifting += UpdateFutureObjects;
    }

    private void UpdateFutureObjects(TimeShift.CurrentTime currentTime)
    {
        if (currentTime != TimeShift.CurrentTime.Future || !_positionUpdated)
            return;

        linkedFutureObj.transform.position = transform.position;
        _positionUpdated = false;
    }
}
