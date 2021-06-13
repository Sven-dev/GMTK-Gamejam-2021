using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerable : MonoBehaviour
{
    [SerializeField] private GameObject[] VisualCues;

    protected bool isPowered = false;

    public virtual void PowerUp()
    {
        isPowered = true;

        PowerVisualCues();
    }
    public virtual void PowerDown()
    {
        isPowered = false;

        PowerVisualCues();
    }



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