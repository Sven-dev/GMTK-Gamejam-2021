using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] private PlugDetector PlugDetector;
    [SerializeField] private CableTripper CableTripper;
    [SerializeField] private Rigidbody Rigidbody;

    private Plug Plug = null;

    public void Grab()
    {
        //Get the closest plug, if there is any
        Plug = PlugDetector.GetClosestPlug();
        if (Plug != null)
        {
            //Disconnect the plug from the socket
            if (Plug.Socket != null)
            {
                Plug.PullOut();
            }

            //Attach the joint to the player
            Plug.Joint.connectedBody = Rigidbody;
            Plug.Joint.connectedAnchor = (Vector3.up + Vector3.back) * 0.15f;

            //Attach the object to the player's back
            Plug.transform.parent = Rigidbody.transform;
            Plug.transform.localPosition = Vector3.zero;
            Plug.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void LetGo()
    {
        if (Plug != null)
        {
            //Detach the joint from the player
            Plug.Joint.connectedBody = Plug.SelfConnector;
            Plug.Joint.connectedAnchor = Vector3.zero;

            //Detach the object from the player's back
            Plug.transform.parent = Plug.Cable.transform;
            //Plug.transform.localPosition = new Vector3(Plug.transform.localPosition.x, 0, Plug.transform.localPosition.z);
            Plug.transform.eulerAngles = Vector3.zero;

            //Plug the cable into a socket
            Socket socket = PlugDetector.GetClosestSocket();
            if (socket != null)
            {
                Plug.PlugIn(socket);
                CableTripper.DisableTrip();
            }
         
            Plug = null;
        }
    }
}