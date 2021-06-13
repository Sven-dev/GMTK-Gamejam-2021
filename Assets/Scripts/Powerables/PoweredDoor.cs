using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredDoor : Powerable
{
    [SerializeField] private Transform door;
    [Space]
    [SerializeField] private Transform opened;
    [SerializeField] private Transform closed;

    private IEnumerator coroutine;
    
    public override void PowerUp()
    {
        base.PowerUp();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = _ToggleDoor(closed, opened);
        StartCoroutine(coroutine);
    }

    public override void PowerDown()
    {
        base.PowerDown();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = _ToggleDoor(opened, closed);
        StartCoroutine(coroutine);
    }

    IEnumerator _ToggleDoor(Transform from, Transform to)
    {
        float progress = 0;
        while (progress < 1)
        {
            door.position = Vector3.Lerp(from.position, to.position, progress);

            progress += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}