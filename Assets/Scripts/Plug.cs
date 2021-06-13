using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] public SocketType Type = SocketType.Universal;
    public Socket socket { get; private set; }


    public void Connect(Socket _socket)
    {
        if (socket != null)
            Disconnect();

        if (_socket != null)
        {
            if (Type == SocketType.Universal || _socket.Type == SocketType.Universal || _socket.Type == Type)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.position = _socket.transform.position;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }

                Joint j = GetComponent<Joint>();
                if (j != null)
                {
                    j.connectedMassScale = 80.0f;
                }

                socket = _socket;
                socket.ConnectPlug(transform);
            }
        }
    }
    public void Disconnect()
    {
        if(socket != null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
            Joint j = GetComponent<Joint>();
            if (j != null)
            {
                j.connectedMassScale = 1.0f;
            }


            socket.PullPlug();
            socket = null;
        }

    }

}
