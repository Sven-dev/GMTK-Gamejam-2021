using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Direction;

    private void Start()
    {
        StartCoroutine(_Rotate());
    }

    private IEnumerator _Rotate()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            Quaternion start = transform.rotation;
            Quaternion end = Quaternion.Euler(transform.eulerAngles + Direction * Vector3.up * 90);

            float progress = 0;
            while (progress < 1)
            {
                transform.rotation = Quaternion.Lerp(start, end, progress);
                progress += Speed * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
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