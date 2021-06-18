using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] public Connector Connector = Connector.Universal;
    [SerializeField] public Cable Cable;
    [Space]
    [SerializeField] public Joint Joint;
    [SerializeField] public Rigidbody SelfConnector;
    [SerializeField] public Rigidbody Rigidbody;

    [HideInInspector] public Socket Socket;

    /// <summary>
    /// Plugs the cable into the socket if it has a compatible connector
    /// </summary>
    /// <param name="socket">The socket the cable tries to plug into</param>
    public void PlugIn(Socket socket)
    {
        if (Connector == Connector.Universal || socket.Type == Connector.Universal || socket.Type == Connector)
        {
            //Attach the joint to the plug
            Joint.connectedBody = socket.Rigidbody;
            transform.position = socket.transform.position;

            //Set logic
            Socket = socket;
            Socket.Connect();
        }
    }

    /// <summary>
    /// Pulls the cable out of the socket
    /// </summary>
    public void PullOut()
    {
        //Detach the joint from the plug
        Joint.connectedBody = SelfConnector;

        //Set logic
        Socket.Disconnect();
        Socket = null;
    }
}