using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] public SocketType Type;
    [SerializeField] public Transform Hole;

    public void ConnectPlug(Transform plug)
    {
        Hole = plug;
    }

    public void PullPlug()
    {
        Hole = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum SocketType
{
    Square,
    Round,
    Triangle,
    Universal
}