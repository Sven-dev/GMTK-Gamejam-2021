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
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.position = _socket.transform.position;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
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
