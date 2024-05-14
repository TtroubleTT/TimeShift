using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask interactableLayer;
    
    [Header("Interact Info")]
    [SerializeField] private float rayCastDistance = 4f;
    [SerializeField] private float interactCooldown = 0.2f;
    private float lastUsed;

    private void DoInteract()
    {
        Vector3 camPos = cam.position;
        Vector3 camForward = cam.forward;

        bool hitObj = Physics.Raycast(camPos + (-camForward * 2.5f),camForward, out RaycastHit hitInfo, rayCastDistance, interactableLayer);
        Debug.Log(hitInfo);
        if (hitObj)
        {
            Debug.Log("hit button");
        }
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        // Cooldown for interaction
        if (Time.time - lastUsed < interactCooldown)
            return;
        
        Debug.Log("Do Interact");
        DoInteract();
        lastUsed = Time.time;
    }
}
