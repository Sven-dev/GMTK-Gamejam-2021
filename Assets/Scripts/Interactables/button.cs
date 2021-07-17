using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : Interactable
{
    private bool Interacted = false;

    public override void Interact()
    {
        if (!Interacted)
        {
            //Do thing
        }
    }
}
