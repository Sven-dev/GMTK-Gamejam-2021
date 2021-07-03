using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator Transition;
    [SerializeField] private float TransitionTime;
    [Space]
    [SerializeField] private int LevelIndex;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(_LoadLevel(LevelIndex));
    }

    private IEnumerator _LoadLevel(int index)
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(LevelIndex);
    }
}
