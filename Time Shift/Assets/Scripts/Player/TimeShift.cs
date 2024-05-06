using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class TimeShift : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Drag drag;
    
    [Header("TimeLines")]
    [SerializeField] private GameObject present;
    [SerializeField] private GameObject future;
    private CurrentTime currentTime = CurrentTime.Present;

    [Header("Time Shift Effect")]
    [SerializeField] private PostProcessVolume timeShiftEffect;
    [SerializeField] private float transitionTime;
    [SerializeField] private float shiftCooldown;
    private float lastUsed;
    
    [Header("Future Effect")]
    [SerializeField] private PostProcessVolume futureEffect;

    public Action <CurrentTime> OnTimeShifting;

    public enum CurrentTime
    {
        Present,
        Future,
    }

    public CurrentTime GetCurrentTime()
    {
        return currentTime;
    }

    public void UpdateCurrentTime(CurrentTime newTime)
    {
        currentTime = newTime;
    }

    public void DoTimeShift()
    {
        drag.StopDragging();
        
        if (currentTime == CurrentTime.Present)
        {
            ShiftToFuture();
        }
        else
        {
            ShiftToPresent();
        }

        lastUsed = Time.time;
        ChangeTimeLinesEffect();
        OnTimeShifting?.Invoke(currentTime);
    }

    private void Start()
    {
        // Starts in the present
        ShiftToPresent();
        TurnOffEffectVolume();
    }

    private void OnTimeShift(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        // Cooldown for ability
        if (Time.time - lastUsed < shiftCooldown)
            return;
        
        DoTimeShift();
    }

    private void ShiftToFuture()
    {
        future.SetActive(true);
        present.SetActive(false);
        currentTime = CurrentTime.Future;
        futureEffect.weight = 1;
    }

    private void ShiftToPresent()
    {
        future.SetActive(false);
        present.SetActive(true);
        currentTime = CurrentTime.Present;
        futureEffect.weight = 0;
    }

    private void ChangeTimeLinesEffect()
    {
        timeShiftEffect.weight = 1;
        Invoke(nameof(TurnOffEffectVolume), transitionTime);
    }
    
    private void TurnOffEffectVolume()
    {
        timeShiftEffect.weight = 0;
    }
}
