using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private AnimationCurve SpeedCurve;
    [Space]
    [SerializeField] private Transform Horizontal;
    [SerializeField] private Transform Vertical;

    [HideInInspector] public Vector2 RotateInput;

    private float Multiplier = 0;

    // Update is called once per frame
    void Update()
    {
        Multiplier = SetSpeedMultiplier(Multiplier, RotateInput);

        if (Multiplier > 0)
        {
            if (RotateInput.x != 0)
            {
                RotateHorizontal();
            }

            if (RotateInput.y != 0)
            {
                RotateVertical();
            }
        }
    }

    private void RotateHorizontal()
    {
        //Rotate left and right
        Vector3 direction = Vector3.zero;
        if (RotateInput.x > 0)
        {
            direction = Vector3.down;
        }
        else if (RotateInput.x < 0)
        {
            direction = Vector3.up;
        }

        Horizontal.Rotate(direction * Speed * Multiplier * Time.deltaTime);
    }

    private void RotateVertical()
    {
        //Rotate up and down
        Vector3 direction = Vector3.zero;
        if (RotateInput.y > 0)
        {
            direction = Vector3.right;
        }
        else if (RotateInput.y < 0)
        {
            direction = Vector3.left;
        }

        Vertical.Rotate(direction * Speed * Multiplier * Time.deltaTime);

        //Clamp the rotation to not go out of bounds
        Vertical.localEulerAngles = new Vector3(Mathf.Clamp(Vertical.localEulerAngles.x, 10, 80), 0, 0);
    }

    /// <summary>
    /// Sets the current speed based on what is being input
    /// </summary>
    private float SetSpeedMultiplier(float s, Vector2 input)
    {
        //Speed up or down depending on if the buttons are being pressed
        if (RotateInput != Vector2.zero)
        {
            s += 2 * Time.deltaTime;
        }
        else
        {
            s -= 2 * Time.deltaTime;
        }

        //Make sure the speed value stays between 0 and 1
        s = Mathf.Clamp(s, 0, 1);
        return s;
    }
}