using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] public SocketType Type;
    [Space]
    [SerializeField] public Transform Beginning;
    [SerializeField] public Transform End;
    [Space]
    [SerializeField] public int Length;

    public void DetachPlug()
    {

    }


}
