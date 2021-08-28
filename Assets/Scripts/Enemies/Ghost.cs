using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [SerializeField] private Transform Target;

    private bool Haunting = true;

    public void Activate()
    {
        gameObject.SetActive(true);

        Haunting = true;
        StartCoroutine(Haunt());
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Haunting = false;
    }

    private IEnumerator Haunt()
    {
        while (Haunting)
        {
            Quaternion q = Quaternion.LookRotation(Target.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 40 * Time.fixedDeltaTime);

            transform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime, Space.Self);
            yield return new WaitForFixedUpdate();
        }
    }
}
