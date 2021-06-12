using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerableFinish : Powerable
{
    [SerializeField] private int NextSceneIndex;
    [Space]
    [SerializeField] private SpriteRenderer FadeImage;


    public override void PowerUp()
    {
        StartCoroutine(_Fade(0, 1));
    }

    public override void PowerDown()
    {
        
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
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

        LoadScene(NextSceneIndex);
    }
}
