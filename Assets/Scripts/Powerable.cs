using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerable : MonoBehaviour
{
    [SerializeField] private GameObject[] VisualCues;

    protected bool isPowered = false;
    public abstract void PowerUp();
    public abstract void PowerDown();


    protected void PowerVisualCues()
    {
        if (VisualCues != null && VisualCues.Length > 0)
        {
            foreach (GameObject visualCue in VisualCues)
            {
                visualCue.SetActive(isPowered);
            }
        }
    }

}