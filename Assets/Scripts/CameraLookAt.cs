using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    private void Start()
    {
        if (Camera.main != null)
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.worldCamera = Camera.main;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMainCam();
    }

    private void LookAtMainCam()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
