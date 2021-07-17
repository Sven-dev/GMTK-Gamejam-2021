using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : Interactable
{
    [SerializeField] private Transform Rotatable;
    [SerializeField] private Axis Axis;
    [SerializeField] private float Speed;

    private bool Rotating;

    public override void Interact()
    {

    }

    private void Update()
    {
       if (!Rotating && Input.sqrMagnitude > 0.25f)
        {
            Vector2 direction = Vector3.zero;
            switch(Axis)
            {
                case Axis.X:
                    direction = Vector3.right;
                    break;
                case Axis.Y:
                    direction = Vector3.up;
                    break;
                case Axis.Z:
                    direction = Vector3.forward;                   
                    break;
            }

            StartCoroutine(_Rotate(direction * Mathf.Abs(Input.x)));
        }
    }

    private IEnumerator _Rotate(Vector3 direction)
    {
        Rotating = true;

        Vector3 start = Rotatable.eulerAngles;
        Vector3 end = start + direction;

        float progress = 0;
        while (progress < 1)
        {
            Rotatable.eulerAngles = Vector3.Lerp(start, end, progress);

            progress += Speed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Rotating = false;
    }

}

public enum Axis
{
    X,
    Y,
    Z
}