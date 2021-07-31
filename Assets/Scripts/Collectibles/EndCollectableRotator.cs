using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollectableRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Rotate());
        StartCoroutine(_Bob());
    }

    private IEnumerator _Rotate()
    {
        while (true)
        {
            transform.eulerAngles += Vector3.up * 100 * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator _Bob()
    {
        Vector3 position = transform.position;
        while (true)
        {
            float temp = Mathf.PingPong(Time.time / 10, 0.25f);
            transform.position = position + Vector3.up * temp;
            yield return null;
        }
    }
}
