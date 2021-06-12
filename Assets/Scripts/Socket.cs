using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] SocketType Type;
    [SerializeField] Transform Hole;
    [SerializeField] Transform Plug;
    [Space]
    [SerializeField] List<Powerable> Powers = new List<Powerable>();

    [SerializeField] bool Powerup;
    [SerializeField] bool Powerdown;

    public bool Plugged;

    private void Update()
    {
        if (Powerup)
        {
            ConnectPlug(transform);
            Powerup = false;
        }

        if (Powerdown)
        {
            PullPlug();
            Powerdown = false;
        }
    }

    public void ConnectPlug(Transform plug)
    {
        Plug = plug;

        plug.parent = Hole;
        plug.transform.position = Hole.position;

        foreach(Powerable powerable in Powers)
        {
            powerable.PowerUp();
        }

        Plugged = true;
    }

    public void PullPlug()
    {
        if (Plug != null)
        {
            Plug.parent = null;
            Plug = null;
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