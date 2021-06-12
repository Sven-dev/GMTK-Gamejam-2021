using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugDetector : MonoBehaviour
{
    private List<Transform> Plugs = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plug")
        {
            Plugs.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plug")
        {
            Plugs.Remove(other.transform);
        }
    }

    //Grab the nearest cable plug, if there's one in range
    public Rigidbody GrabPlug()
    {
        Rigidbody plugRb = null;
        if (Plugs.Count > 0)
        {
            Transform plugTr = GetClosestPlug();
            plugRb = plugTr.GetComponent<Rigidbody>();
        }
        return plugRb;
    }

    private Transform GetClosestPlug()
    {
        Transform ClosestPlug = transform;
        float ClosestDistance = Mathf.Infinity;

        foreach (Transform Plug in Plugs)
        {
            float distance = Vector3.Distance(transform.position, Plug.position);

            if (distance < ClosestDistance)
            {
                ClosestPlug = Plug;
                ClosestDistance = distance;
            }
        }

        return ClosestPlug;
    }

    private void DropCable()
    {

    }

    private void PlugCable()
    {

    }
}