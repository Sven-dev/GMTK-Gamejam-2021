using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotator : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [SerializeField] private Transform Point;
    [SerializeField] private Axis Axis;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Move());
    }

    private IEnumerator _Move()
    {
        Vector3 axis = GetAxisVector(Axis);
        while (true)
        {
            transform.RotateAround(Point.position, axis, Speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private Vector3 GetAxisVector(Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return Vector3.right;
            case Axis.Y:
                return Vector3.up;
            case Axis.Z:
                return Vector3.forward;
            default:
                throw new System.Exception("Unknown axis: " + axis);
        }
    }
}
