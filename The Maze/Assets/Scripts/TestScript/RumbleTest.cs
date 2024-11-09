using System;
using UnityEngine;

public class RumbleTest : MonoBehaviour
{
    private void Update()
    {
        if (InputManager.instance.controls.Rumble.RumbleAction.WasPressedThisFrame())
        {
            //rumble
            RumbleManager.instance.RumblePulse(0.25f, 0.5f, 0.25f);
            Debug.Log("rumble");
        }
    }
}
