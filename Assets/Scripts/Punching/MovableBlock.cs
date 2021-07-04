using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [SerializeField] private Transform MoveTo;

    public void Move()
    {
        StartCoroutine(_move());
    }

    private IEnumerator _move()
    {
        Vector3 from = transform.position;
        Vector3 to = MoveTo.position;

        float progress = 0;
        while (progress < 1)
        {
            transform.position = Vector3.Lerp(from, to, progress);

            progress += Speed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        transform.position = to;
    }
}
