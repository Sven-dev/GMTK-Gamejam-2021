using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPowerStation : Powerable
{
    [SerializeField] private int PowerNeedMax = 4;

    private int PowerNeed = 0;

    public override void PowerUp()
    {
        PowerNeed++;

        CheckPower();
    }
    public override void PowerDown()
    {
        PowerNeed--;

        CheckPower();
    }

    private void CheckPower()
    {
        Powered = PowerNeed == PowerNeedMax;
    }

}
