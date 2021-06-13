using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] public Plug plugEnd;

    public int Length;

    public void DetachPlug()
    {
        if (plugEnd != null && plugEnd.IsConnected() && plugEnd.sensitive)
        {
            plugEnd.Disconnect();
        }
    }


}
