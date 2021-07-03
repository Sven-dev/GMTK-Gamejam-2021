using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerableBlockMover : Powerable
{
    [SerializeField] private Transform Block;
    [Space]
    [SerializeField] private Transform From;
    [SerializeField] private Transform To;
    [Space]
    [SerializeField] private float Movetime;

    private IEnumerator _Coroutine;

    public override void PowerUp()
    {
        base.PowerUp();
        if (_Coroutine != null)
        {
            StopCoroutine(_Coroutine);
        }

        _Coroutine = _Move(From.position, To.position);
        StartCoroutine(_Coroutine);
    }

    public override void PowerDown()
    {
        base.PowerDown();
        if (_Coroutine != null)
        {
            StopCoroutine(_Coroutine);
        }

        _Coroutine = _Move(To.position, From.position);
        StartCoroutine(_Coroutine);
    }

    private IEnumerator _Move(Vector3 from, Vector3 to)
    {
        float progress = 0;
        while(progress > 1)
        {
            Block.position = Vector3.Lerp(from, to, progress);

            progress += Movetime * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}