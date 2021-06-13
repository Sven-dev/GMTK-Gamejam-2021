using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] Image darknessImage;

    public static CanvasManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }


    public IEnumerator FadeToBlack(float from, float to)
    {
        float progress = 0;
        while (progress < 1)
        {
            Color c = darknessImage.color;
            c.a = Mathf.Lerp(from, to, progress);
            darknessImage.color = c;

            progress += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        FinishFade.Invoke();
    }

    public event Action FinishFade;
}
