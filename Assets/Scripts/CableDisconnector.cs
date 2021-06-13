using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableDisconnector : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cable"))
        {
            Cable cable = other.transform.GetComponent<Cable>();
            if (cable != null)
            {
                cable.DetachPlug();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cable"))
        {
            Cable cable = other.transform.GetComponent<Cable>();
            if (cable != null)
            {

            }
        }
    }
}
