using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [SerializeField] private Transform[] MovePoints;

    void Start()
    {
        StartCoroutine(_Move());
    }

    private IEnumerator _Move()
    {
        int CurrentPoint = 0;
        while (true)
        {
            Vector3 start = MovePoints[CurrentPoint].position;
            Vector3 end;
            if (CurrentPoint == MovePoints.Length - 1)
            {
                end = MovePoints[0].position;
                CurrentPoint = -1;
            }
            else
            {
                end = MovePoints[CurrentPoint + 1].position;
            }

            float progress = 0;
            while (progress < 1)
            {
                transform.position = Vector3.Lerp(start, end, progress);
                progress += Speed * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(3f);
            CurrentPoint++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == transform)
        {
            other.transform.parent = null;
        }
    }
}
