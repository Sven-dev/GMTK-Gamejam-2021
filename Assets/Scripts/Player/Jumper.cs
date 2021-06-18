using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask = 0;
    [SerializeField] private float JumpStrength = 3.5f;
    [SerializeField] private Rigidbody Rigidbody;

    public void Jump()
    {
        if (Grounded())
        {
            Rigidbody.velocity += Vector3.up * JumpStrength;
        }
    }

    private bool Grounded()
    {
        Vector3 halfExtend = new Vector3(0.15f, 0.1f, 0.15f);
        return Physics.CheckBox(transform.position, halfExtend, transform.rotation, groundMask);
    }
}