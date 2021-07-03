using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerable : MonoBehaviour
{
    protected bool Powered = false;

    public virtual void PowerUp()
    {
        Powered = true;
    }

    public virtual void PowerDown()
    {
        Powered = false;
    }
}