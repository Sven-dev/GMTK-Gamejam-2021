using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer FadeImage;

    private void Awake()
    {
        Color c = FadeImage.color;
        c.a = 1;
        FadeImage.color = c;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Fade(1, 0));
    }

    IEnumerator _Fade(float from, float to)
    {
        float progress = 0;
        while (progress < 1)
        {
            Color c = FadeImage.color;
            c.a = Mathf.Lerp(from, to, progress);
            FadeImage.color = c;

            progress += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
