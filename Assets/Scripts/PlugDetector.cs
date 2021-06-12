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
            print("plug detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plug")
        {
            Plugs.Remove(other.transform);
            print("plug removed");
        }
    }

    //Grab the nearest cable plug, if there's one in range
    public void GrabPlug()
    {
        if (Plugs.Count != 0)
        {
            Transform Plug = GetClosestPlug();


        }
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