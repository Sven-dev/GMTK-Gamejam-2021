using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollectableRotator : MonoBehaviour
{
    [SerializeField] private int LevelIndex;
    [SerializeField] private Collider Collider;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Rotate());
        StartCoroutine(_Bob());
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider.enabled = false;

        PlayerController.Instance.DisableControls();
        LevelManager.Instance.LoadLevel(LevelIndex, Transition.Crossfade);
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
