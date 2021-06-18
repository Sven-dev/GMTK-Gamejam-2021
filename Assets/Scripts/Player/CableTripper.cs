using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableTripper : MonoBehaviour
{
    [SerializeField] private float DisableTime = 1f;
    [SerializeField] private Collider Collider;

    private void OnTriggerEnter(Collider other)
    {
        CheckPlug(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckPlug(other);
    }

    private void CheckPlug(Collider other)
    {
        if (other.CompareTag("Cable"))
        {
            Cable cable = other.transform.parent.parent.GetComponent<Cable>();
            if (cable.Plug.Socket != null)
            {
                cable.Plug.PullOut();
            }
        }
    }

    public void DisableTrip()
    {
        StartCoroutine(_DisableTrip(DisableTime));
    }

    private IEnumerator _DisableTrip(float time)
    {
        Collider.enabled = false;
        yield return new WaitForSeconds(time);
        Collider.enabled = true;
    }

}