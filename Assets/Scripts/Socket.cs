using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] public SocketType Type;
    [SerializeField] Transform Hole;
    [SerializeField] Transform PlugTr;
    [Space]
    [SerializeField] List<Powerable> Powers = new List<Powerable>();

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