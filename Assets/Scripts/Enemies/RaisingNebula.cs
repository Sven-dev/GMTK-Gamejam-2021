using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingNebula : MonoBehaviour
{
    [SerializeField] private Axis Axis;
    [SerializeField] private float Duration = 1;
    [SerializeField] private float Height = 1;
    [SerializeField] private bool Looping = false;
    [Space]
    [SerializeField] private AnimationCurve Curve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Raise());
    }

    private IEnumerator _Raise()
    {
        Vector3 direction = GetAxisVector(Axis);
        Vector3 temp = Vector3.one - direction;

        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = direction * Curve.Evaluate(progress) * Height + temp;
            progress += Time.fixedDeltaTime / Duration;
            yield return new WaitForFixedUpdate();
        }

        if (Looping)
        {
            StartCoroutine(_Raise());
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