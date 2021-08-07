using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Direction;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Direction * Speed * Time.fixedDeltaTime);

        foreach (Transform item in transform)
        {
            item.Translate(Vector3.up * Direction * Speed * Time.fixedDeltaTime, transform);
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
