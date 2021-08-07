using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private Collider Collider;
    [SerializeField] private Renderer Renderer;

    private bool Falling = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!Falling)
        {
            Falling = true;
            StartCoroutine(_Blink());
        }
    }

    /// <summary>
    /// Show an indicator that the platform is going to fall for x seconds
    /// </summary>
    private IEnumerator _Blink()
    {
        float progress = 0;
        while (progress < 1)
        {
            Renderer.enabled = !Renderer.enabled;

            progress += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(_Fall());
    }

    /// <summary>
    /// Disable the platform for x seconds
    /// </summary>
    private IEnumerator _Fall()
    {
        Renderer.enabled = false;
        Collider.enabled = false;
        yield return new WaitForSeconds(5);
        Renderer.enabled = true;
        Collider.enabled = true;

        Falling = false;
    }
}