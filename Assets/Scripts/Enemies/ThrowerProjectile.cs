using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerProjectile : MonoBehaviour
{
    [HideInInspector] public float Distance;
  
    [SerializeField] private float Speed = 1;
    [SerializeField] private float Height = 2;

    void Start()
    {
        StartCoroutine(_Move());
    }

    private IEnumerator _Move()
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.down * 0.5f + transform.forward * Distance;

        float progress = 0;
        while (progress < 1)
        {
            transform.position = MathParabola.Parabola(start, end, Height, progress);

            progress += Time.deltaTime * Speed;
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}