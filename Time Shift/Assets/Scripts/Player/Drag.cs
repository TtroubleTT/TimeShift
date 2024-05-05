using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float rayCastWidth = 2.5f;
    [SerializeField] private float rayCastDistance = 6f;

    private GameObject _currentDrag;

    public GameObject GetCurrentDrag()
    {
        return _currentDrag;
    }

    public void StopDragging()
    {
        _currentDrag = null;
    }
    
    public void OnDrag(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            Vector3 camPos = cam.position;
            Quaternion camRot = cam.rotation;
        
            bool hitObj = Physics.BoxCast(camPos + (-cam.forward * 2.5f), new Vector3(rayCastWidth, rayCastWidth, rayCastWidth), cam.forward, out RaycastHit hitInfo, camRot, rayCastDistance);
            if (hitObj && hitInfo.transform.gameObject.CompareTag("Draggable"))
            {
                _currentDrag = hitInfo.transform.gameObject;
            }

            return;
        }

        if (context.canceled)
        {
            _currentDrag = null;
        }
    }
}
