using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [HideInInspector] public Interactable Object;
    [HideInInspector] public Vector2 Input;

    public void Interact()
    {
        Object.Interact();
    }

    public bool Interactable()
    {
        //To do: Detect if there is an interactible in range
        //Right now it uses ontriggerenter/exit but it's more efficient to do with a spherecast
        if (Object != null)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Object = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit(Collider other)
    {
        Object = null;
    }
}