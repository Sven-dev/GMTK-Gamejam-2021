using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] public SocketType Type;
    [SerializeField] private Transform Hole;
    [SerializeField] private Transform PlugTr;
    [SerializeField] public Rigidbody Rigidbody;
    [Space]
    [SerializeField] List<Powerable> Powers = new List<Powerable>();
    [Space]
    [SerializeField] private AudioSource PlugIn;
    [SerializeField] private AudioSource PlugOut;

    public bool Plugged;

    public void ConnectPlug(Transform plug)
    {
        PlugTr = plug;

        //plug.parent = Hole;
        plug.transform.position = transform.position;

        foreach(Powerable powerable in Powers)
        {
            powerable.PowerUp();
        }

        PlugIn.Play();
        Plugged = true;
    }

    public void PullPlug()
    {
        if (PlugTr != null)
        {
            // PlugTr.parent = null;
            PlugTr = null;
        }

        foreach (Powerable powerable in Powers)
        {
            powerable.PowerDown();
        }

        PlugOut.Play();
        Plugged = false;
    }
}

public enum SocketType
{
    Square,
    Round,
    Triangle,
    Universal
}