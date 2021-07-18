using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : Interactable
{
    [SerializeField] private Transform Block;
    [SerializeField] private Transform MoveTo;

    private bool Interacted = false;

    public override void StartInteract()
    {
        if (!Interacted)
        {
            Interacted = true;
            StartCoroutine(_Move());
        }
    }

    public override void StopInteract()
    {
        //Doesn't need any functionality
    }

    private IEnumerator _Move()
    {
        Vector3 from = Block.transform.position;
        Vector3 to = MoveTo.position;

        float progress = 0;
        while (progress < 1)
        {
            Block.transform.position = Vector3.Lerp(from, to, progress);

            progress += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}