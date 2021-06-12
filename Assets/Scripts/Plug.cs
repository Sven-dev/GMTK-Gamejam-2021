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
                socket = _socket;
                socket.ConnectPlug(transform);
            }
        }
    }
    public void Disconnect()
    {
        if(socket != null)
        {
            socket.PullPlug();
            socket = null;
        }

    }

}
