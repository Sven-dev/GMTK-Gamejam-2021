using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMover : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [Space]
    [SerializeField] private List<Transform> Points;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Move());
    }

    private IEnumerator _Move()
    {
        int pointIndex = 0;
        while (true)
        {
            Vector3 start = Points[pointIndex].position;
            Vector3 end = Points[Next(pointIndex)].position;

            float progress = 0;
            while (progress < 1)
            {
                transform.position = Vector3.Lerp(start, end, progress);

                progress += Time.fixedDeltaTime * Speed;
                yield return new WaitForFixedUpdate();
            }

            pointIndex = Next(pointIndex);
        }
    }

    private int Next(int index)
    {
        if (index + 1 == Points.Count )
        {
            return 0;
        }

        return index + 1;
    }
}