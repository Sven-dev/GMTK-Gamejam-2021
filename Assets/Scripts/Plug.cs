using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] public Connector Connector = Connector.Universal;
    [SerializeField] public CableSource Source;
    [SerializeField] public Cable Cable;
    [SerializeField] public Rigidbody Rigidbody;

    public Socket Socket;

    /// <summary>
    /// Plugs the cable into the socket if it has a compatible connector
    /// </summary>
    /// <param name="socket">The socket the cable tries to plug into</param>
    public void PlugIn(Socket socket)
    {
        if (Connector == Connector.Universal || socket.Type == Connector.Universal || socket.Type == Connector)
        {
            //Attach the joint to the plug
            Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = socket.transform.position;
            Socket = socket;
            Socket.Connect();

            Cable.DisableTripping();
        }
    }

    /// <summary>
    /// Pulls the cable out of the socket
    /// </summary>
    public void PullOut()
    {
        //Detach the joint from the plug
        Socket.Disconnect();
        Socket = null;

        Cable.Trippable = false;
    }
}