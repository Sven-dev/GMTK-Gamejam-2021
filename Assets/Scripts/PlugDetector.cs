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
            Plug plig = plug.GetComponent<Plug>();

            if (s != null && !s.Plugged && plig != null)
            {
                plig.Connect(s);
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