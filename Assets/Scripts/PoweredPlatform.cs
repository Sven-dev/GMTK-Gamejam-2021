using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredPlatform : Powerable
{
    [SerializeField] private float Speed = 1;
    [Space]
    [SerializeField] private Rigidbody Platform;
    [Space]
    [SerializeField] private Transform Beginning;
    [SerializeField] private Transform End;

    private bool active = false;

    private void Start()
    {
        StartCoroutine(_Move());
    }

    public override void PowerUp()
    {
        active = true;
    }

    public override void PowerDown()
    {
        active = false;
    }

    IEnumerator _Move()
    {
        int sigma = 1;
        float progress = 0;
        while(true)
        {
            if (active)
            {
                Platform.position = Vector3.Lerp(Beginning.position, End.position, progress);

                progress += Time.fixedDeltaTime * Speed * sigma;
                if (progress >= 1)
                {
                    sigma = -1;
                }
                else if (progress <= 0)
                {
                    sigma = 1;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}