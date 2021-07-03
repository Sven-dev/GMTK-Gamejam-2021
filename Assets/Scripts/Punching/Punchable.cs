using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Punchable : MonoBehaviour
{
    protected bool Punched = false;

    public virtual void Punch()
    {
        Punched = true;
    }
}