using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] public Connector Type;
    [SerializeField] public Rigidbody Rigidbody;
    [Space]
    [SerializeField] private List<Powerable> Powers = new List<Powerable>();

    [HideInInspector] public bool Plugged;

    public void Connect()
    {
        Plugged = true;
        foreach (Powerable powerable in Powers)
        {
            powerable.PowerUp();
        }
    }

    public void Disconnect()
    {
        Plugged = false;
        foreach (Powerable powerable in Powers)
        {
            powerable.PowerDown();
        }
    }
}

public enum Connector
{
    Blue,
    Red,
    Yellow,
    Universal
}