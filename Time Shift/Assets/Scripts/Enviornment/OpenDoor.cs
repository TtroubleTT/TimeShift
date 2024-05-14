using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : ObjectsInteraction
{
    [SerializeField] private Animator animator;
    private DoorState _doorState = DoorState.Closed;
    
    public enum DoorState
    {
        Open,
        Closed,
    }
    
    public override void DoInteraction()
    {
        if (_doorState == DoorState.Closed)
        {
            animator.Play("DoorOpen");
            _doorState = DoorState.Open;
        }
        else
        {
            animator.Play("DoorClose");
            _doorState = DoorState.Closed;
        }
    }
}
