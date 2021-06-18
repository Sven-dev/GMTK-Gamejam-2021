using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] private PlugDetector PlugDetector;
    [SerializeField] private Transform PlugHolder;

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

            //Attach the object to the player's back
            Plug.transform.parent = PlugHolder;
            Plug.transform.localPosition = Vector3.zero;
            Plug.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Plug.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void LetGo()
    {
        if (Plug != null)
        {
            //Detach the object from the player's back
            Plug.transform.parent = Plug.Source.transform;
            Plug.transform.eulerAngles = Vector3.zero;
            Plug.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            //Plug the cable into a socket
            Socket socket = PlugDetector.GetClosestSocket();
            if (socket != null)
            {
                Plug.PlugIn(socket);
            }
         
            Plug = null;
        }
    }
}