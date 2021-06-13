using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EndGateFinisher : MonoBehaviour
{
    [SerializeField] private int NextSceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
        if (NextSceneIndex >= 0)
        {
            SceneManager.LoadScene(NextSceneIndex);
        }
        else
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
