using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [HideInInspector] public Interactable Object;
    public Vector2 Input 
    {
        set
        {
            if (Object != null)
            {
                Object.Input = value;
            }
        }
    }

    public void StartInteract()
    {
        Object.StartInteract();
    }

    public void StopInteract()
    {
        Object.StopInteract();
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
        print(other.name);
        Object = other.transform.GetComponent<Interactable>();
    }

    private void OnTriggerExit(Collider other)
    {
        Object = null;
    }
}