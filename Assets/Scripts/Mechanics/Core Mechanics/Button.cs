using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Transform Block;
    [SerializeField] private Transform MoveTo;

    private bool Pressed = false;

    public void Press()
    {
        Pressed = true;
        StartCoroutine(_Move());
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

    private void OnTriggerEnter(Collider other)
    {
        if (!Pressed)
        {
            Press();
        }
    }
}