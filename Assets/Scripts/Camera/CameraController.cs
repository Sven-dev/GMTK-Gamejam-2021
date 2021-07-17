using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float BorderThickness = 10f;
    [SerializeField] private float Speed = 20f;
    [Space]
    [SerializeField] private Transform Horizontal;
    [SerializeField] private Transform Vertical;
    [Space]
    [SerializeField] private Transform Target;

    [HideInInspector] public Vector2 RotateInput = new Vector2(Screen.width/2, Screen.height/2);

    private bool Rotating = false;

    void Update()
    {
        transform.position = Target.position;

        bool rotating = false;
        if (RotateInput.y >= Screen.height - BorderThickness)
        {
            RotateVertical(1);
            rotating = true;
        }
        else if (RotateInput.y <= BorderThickness)
        {
            RotateVertical(-1);
            rotating = true;
        }

        if (RotateInput.x >= Screen.width - BorderThickness)
        {
            RotateHorizontal(1);
            rotating = true;
        }
        else if (RotateInput.x <= BorderThickness)
        {
            RotateHorizontal(-1);
            rotating = true;
        }

        //Play a sound effect while the camera is rotating
        if (Rotating != rotating)
        {
            Rotating = rotating;
            if (Rotating)
            {
                AudioManager.Instance.SetLoop("CameraRotate", true);
                AudioManager.Instance.Play("CameraRotate");
            }
            else
            {
                AudioManager.Instance.SetLoop("CameraRotate", false);
            }
        }
    }

    public float GetHorizontalRotation()
    {
        return Horizontal.transform.eulerAngles.y;
    }

    private void RotateHorizontal(float amount)
    {
        Horizontal.Rotate(Vector3.up * amount * Speed * Time.deltaTime);
    }

    private void RotateVertical(float amount)
    {
        Vertical.Rotate(Vector3.right * amount * Speed * Time.deltaTime);

        //Clamp the rotation to not go out of bounds
        Vertical.localEulerAngles = new Vector3(Mathf.Clamp(Vertical.localEulerAngles.x, 10, 80), 0, 0);
    }
}