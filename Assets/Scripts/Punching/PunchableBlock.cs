using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableBlock : Punchable
{
    [SerializeField] private Transform PunchedBlock;
    [SerializeField] private Transform MovePunchedBlockTo;

    public override void Punch()
    {
        if (!Punched)
        {
            base.Punch();

            StartCoroutine(_MoveBlock());
        }
    }

    private IEnumerator _MoveBlock()
    {
        Vector3 from = PunchedBlock.position;
        Vector3 to = MovePunchedBlockTo.position;

        float progress = 0;
        while (progress < 1)
        {
            PunchedBlock.position = Vector3.Lerp(from, to, progress);

            progress += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        PunchedBlock.position = to;
    }
}