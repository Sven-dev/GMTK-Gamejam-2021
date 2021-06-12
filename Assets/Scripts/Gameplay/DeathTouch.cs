using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTouch : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.PlayerReset();
            }
        }
    }
}
