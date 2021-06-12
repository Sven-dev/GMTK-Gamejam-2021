using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugDetector : MonoBehaviour
{
    private List<Transform> Plugs = new List<Transform>();
    private List<Transform> Sockets = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plug")
        {
            Plugs.Add(other.transform);
        }

        if (other.tag == "Socket")
        {
            Debug.Log(other.name);

            Sockets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plug")
        {
            Plugs.Remove(other.transform);
        }

        if (other.tag == "Socket")
        {
            Sockets.Remove(other.transform);
        }
    }

    //Grab the nearest cable plug, if there's one in range
    public Rigidbody GrabPlug()
    {
        Rigidbody plugRb = null;
        if (Plugs.Count > 0)
        {
            Transform plugTr = GetClosestItem(Plugs);
            plugRb = plugTr.GetComponent<Rigidbody>();
        }
        return plugRb;
    }

    public void PlugCable(Rigidbody plug)
    {
        if (Sockets.Count > 0)
        {
            Transform socket = GetClosestItem(Sockets);
            Socket s = socket.GetComponent<Socket>();

            print(s == null);
            print(socket.name);

            if (!s.Plugged)
            {
                plug.constraints = RigidbodyConstraints.FreezeAll;
                plug.position = socket.position;
                plug.transform.parent = socket;

                s.Plugged = true;
            }
        }
    }

    private Transform GetClosestItem(List<Transform> items)
    {
        Transform closestItem = transform;
        float ClosestDistance = Mathf.Infinity;

        foreach (Transform item in items)
        {
            float distance = Vector3.Distance(transform.position, item.position);

            if (distance < ClosestDistance)
            {
                closestItem = item;
                ClosestDistance = distance;
            }
        }

        return closestItem;
    }
}