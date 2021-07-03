using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerablePortal : Powerable
{
    [SerializeField] private GameObject PortalAnimation;
    [SerializeField] private Light LightSource;

    public override void PowerUp()
    {
        base.PowerUp();

        PortalAnimation.SetActive(true);
        LightSource.gameObject.SetActive(true);
    }

    public override void PowerDown()
    {
        base.PowerDown();

        PortalAnimation.SetActive(false);
        LightSource.gameObject.SetActive(false);
    }
}