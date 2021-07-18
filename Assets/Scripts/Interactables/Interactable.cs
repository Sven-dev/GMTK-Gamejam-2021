using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [HideInInspector] public Vector2 Input;

    public abstract void StartInteract();

    public abstract void StopInteract();

    private void OnTriggerEnter(Collider other)
    {
        print("On interactable");
    }

    private void OnTriggerExit(Collider other)
    {
        print("Off interactable");
    }
}