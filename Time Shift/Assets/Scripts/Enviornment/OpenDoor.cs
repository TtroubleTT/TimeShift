using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : ObjectsInteraction
{
    [SerializeField] private Animator animator;
    [SerializeField] private float doorTransitionTime = 1f;
    private DoorState _doorState = DoorState.Closed;
    
    public enum DoorState
    {
        Open,
        Closed,
        Moving,
    }
    
    public override void DoInteraction()
    {
        if (_doorState == DoorState.Moving)
            return;
        
        if (_doorState == DoorState.Closed)
        {
            animator.Play("DoorOpen");
            _doorState = DoorState.Moving;
            Invoke(nameof(UpdateOpenState), doorTransitionTime);
        }
        else
        {
            animator.Play("DoorClose");
            _doorState = DoorState.Moving;
            Invoke(nameof(UpdateClosedState), doorTransitionTime);
        }
    }

    public void UpdateOpenState()
    {
        _doorState = DoorState.Open;
    }

    private void UpdateClosedState()
    {
        _doorState = DoorState.Closed;
    }
}
