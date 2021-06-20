using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float Speed;
    [Space]
    [SerializeField] private Transform Horizontal;
    [SerializeField] private Transform Vertical;

    [HideInInspector] public Vector2 RotateInput;

    [SerializeField] private AudioSource RotateSfx;

    private float Multiplier = 0;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(RotateInput.x) > 0.1f)
        {
            RotateHorizontal(RotateInput.x);
        }

        if (Mathf.Abs(RotateInput.y) > 0.1f)
        {
            RotateVertical(RotateInput.y);
        }

        if (!RotateSfx.isPlaying && RotateInput.sqrMagnitude > 0.1f)
        {
            RotateSfx.Play();
        }

        RotateSfx.pitch = 0.75f + RotateInput.sqrMagnitude;
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