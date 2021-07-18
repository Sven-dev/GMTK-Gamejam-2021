using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float Speed = 2.0f;
    [SerializeField] private Rigidbody Rigidbody;

    [HideInInspector] public Vector2 Input = Vector2.zero;

    /// <summary>
    /// Rotate the character towards input direction
    /// </summary>
    void Update()
    {
        if (Input != Vector2.zero)
        {
            Vector3 move = new Vector3(Input.x, 0, Input.y) * Speed * Time.fixedDeltaTime;
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// /Move character into input direction
    /// </summary>
    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.x, 0, Input.y) * Speed * Time.fixedDeltaTime;
        transform.position += move;
    }
}