using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredPlatform : Powerable
{
    [SerializeField] private float Speed = 1;
    [Space]
    [SerializeField] private Transform Platform;
    [Space]
    [SerializeField] private Transform Beginning;
    [SerializeField] private Transform End;

    private void Start()
    {
        StartCoroutine(_Move());
    }

    IEnumerator _Move()
    {
        int sigma = 1;
        float progress = 0;
        while(true)
        {
            if (isPowered)
            {
                Platform.position = Vector3.Lerp(Beginning.position, End.position, progress);

                progress += Time.fixedDeltaTime * Speed * sigma;
                if (progress >= 1)
                {
                    sigma = -1;
                    yield return new WaitForSeconds(2);
                }
                else if (progress <= 0)
                {
                    sigma = 1;
                    yield return new WaitForSeconds(2);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

}