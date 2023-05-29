using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A simpel class for handling the portals flickering effect based on enter and exit triggers
public class FlickerTrigger : MonoBehaviour
{
    [SerializeField] AudioClip reminderDialogClip;
    private bool _playReminderDialog;
    private bool _hasEntered;
    private void OnTriggerEnter(Collider other)
    {
        FlickeringEffect.portalIsFlickering = true;
        if (_playReminderDialog && !_hasEntered)
        {
            _hasEntered = true;
            SoundManager.Instance.PlayDialog(reminderDialogClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FlickeringEffect.portalIsFlickering = false;
        _playReminderDialog = true;
        _hasEntered = false;
    }
}
