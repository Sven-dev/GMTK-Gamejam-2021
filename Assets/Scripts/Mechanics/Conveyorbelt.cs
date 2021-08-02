using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyorbelt : MonoBehaviour
{
    [SerializeField] private float Speed;

    void FixedUpdate()
    {
        foreach(Transform item in transform)
        {
            item.Translate(Vector3.forward * Speed * Time.fixedDeltaTime, transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}