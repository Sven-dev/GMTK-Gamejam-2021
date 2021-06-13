using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerableFinish : Powerable
{
    [Space]
    [SerializeField] private int NextSceneIndex;

    public override void PowerUp()
    {
        isPowered = true;

        PowerVisualCues();
    }
    public override void PowerDown()
    {
        isPowered = false;

        PowerVisualCues();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isPowered)
        {
            ActivateEnd();
        }
    }

    public void ActivateEnd()
    {
        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.FinishFade += () => LoadScene();
            StartCoroutine(CanvasManager.Instance.FadeToBlack(0, 1));
        }
        else
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (NextSceneIndex > 0)
        {
            SceneManager.LoadScene(NextSceneIndex);
        }
    }

}
