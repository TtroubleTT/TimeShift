using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject dragInfo;
    [SerializeField] private GameObject buttonInfo;

    [Header("RayCast")] 
    [SerializeField] private float rayCastDistance = 10f;
    [SerializeField] private LayerMask interactableLayer;
    
    private void Update()
    {
        Vector3 camPos = cam.position;
        Vector3 camForward = cam.forward;

        bool hitObj = Physics.Raycast(camPos + (-camForward * 2.5f),camForward, out RaycastHit hitInfo, rayCastDistance, interactableLayer);
        if (hitObj)
        {
            GameObject obj = hitInfo.transform.gameObject;
            if (obj.CompareTag("Draggable"))
            {
                dragInfo.SetActive(true);
                return;
            }
            
            if (obj.CompareTag("Button"))
            {
                buttonInfo.SetActive(true);
                return;
            }
        }
        
        dragInfo.SetActive(false);
        buttonInfo.SetActive(false);
    }
}
