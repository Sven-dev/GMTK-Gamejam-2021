using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] public SocketType Type = SocketType.Universal;
    [SerializeField] public Joint Joint;
    [SerializeField] public Rigidbody SelfConnector;
    [SerializeField] public Transform CableHolder;
    [Space]
    [SerializeField] private Rigidbody Rigidbody;

    public Socket socket { get; private set; }

    public bool sensitive { get; private set; }

    public IEnumerator DetachSensetive()
    {
        sensitive = false;
        yield return new WaitForSeconds(1.5f);
        sensitive = true;
    }

    public void Connect(Socket _socket)
    {
        if (socket != null)
            Disconnect();

        if (_socket != null)
        {
            if (Type == SocketType.Universal || _socket.Type == SocketType.Universal || _socket.Type == Type)
            {
                if (Rigidbody != null)
                {
                    Rigidbody.position = _socket.transform.position;
                    Joint.connectedBody = _socket.Rigidbody;
                }

                socket = _socket;
                socket.ConnectPlug(transform);

                StartCoroutine(DetachSensetive());
            }
        }
    }

    public bool IsConnected()
    {
        return socket != null;
    }

    public void Disconnect()
    {
        if(socket != null)
        {
            if (Rigidbody != null)
            {
                Joint.connectedBody = SelfConnector;
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