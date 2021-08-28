using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    [SerializeField] private bool SetTo = true;
    [SerializeField] private Ghost[] Ghosts;

    private void OnTriggerEnter(Collider other)
    {
        foreach (Ghost ghost in Ghosts)
        {
            if (SetTo == true)
            {
                ghost.Activate();
            }
            else
            {
                ghost.Deactivate();
            }
        }

        Destroy(gameObject);
    }
}