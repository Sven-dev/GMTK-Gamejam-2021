using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Powerable
{
    [SerializeField] private Light Spotlight;
    [SerializeField] private Light Backlight;


    public override void PowerUp()
    {
        Spotlight.enabled = true;
        Backlight.enabled = true;
    }

    public override void PowerDown()
    {
        Spotlight.enabled = false;
        Backlight.enabled = false;
    }
}