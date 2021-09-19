using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : Interactable
{
    [SerializeField] private Transform Rotatable;
    [SerializeField] private Axis Axis;
    [SerializeField] private float Speed;

    private bool Interacting = false;
    private bool Rotating = false;

    public override void StartInteract()
    {
        Interacting = true;
        StartCoroutine(_Update());
    }

    public override void StopInteract()
    {
        Interacting = false;
    }

    private IEnumerator _Update()
    {
        //Convert the enum value into a usable Vector3
        Vector3 direction = Vector3.zero;
        switch (Axis)
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

        //While the player is interacting with the object, rotate the rotatable based in stick input
        while (Interacting)
        {
            Rotatable.Rotate(Input.x * direction);
            yield return new WaitForFixedUpdate();
        }

        Vector3 start = Rotatable.eulerAngles;
        Vector3 end = new Vector3(
            Mathf.Round(start.x / 90) * 90,
            Mathf.Round(start.y / 90) * 90,
            Mathf.Round(start.z / 90) * 90);

        //Move rotation to nearest 90-degree angle
        float progress = 0;
        while (!Interacting && progress < 1)
        {
            print("rotating back");
            Rotatable.eulerAngles = Vector3.Lerp(start, end, progress);

            progress += 3 * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Rotatable.eulerAngles = Vector3.Lerp(start, end, 1);
    }
}

public enum Axis
{
    X,
    Y,
    Z
}