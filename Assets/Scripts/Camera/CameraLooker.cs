using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooker : MonoBehaviour
{
    [SerializeField] private Transform Target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target);
    }
}
