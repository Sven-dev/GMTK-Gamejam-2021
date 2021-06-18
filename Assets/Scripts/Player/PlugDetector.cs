using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugDetector : MonoBehaviour
{
    private List<Transform> Plugs = new List<Transform>();
    private List<Transform> Sockets = new List<Transform>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plug") Plugs.Add(other.transform);
        if (other.tag == "Socket") Sockets.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plug") Plugs.Remove(other.transform);
        if (other.tag == "Socket") Sockets.Remove(other.transform);
    }

    /// <summary>
    /// Gets the nearest Plug, if there's one in range
    /// </summary>
    public Plug GetClosestPlug()
    {
        if (Plugs.Count > 0)
        {
            return GetClosestItem(Plugs).GetComponent<Plug>();
        }

        return null;
    }

    /// <summary>
    /// Gets the nearest Socket, if there's one in range
    /// </summary>
    public Socket GetClosestSocket()
    {
        if (Sockets.Count > 0)
        {
            return GetClosestItem(Sockets).GetComponent<Socket>();
        }

        return null;
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