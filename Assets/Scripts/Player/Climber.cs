using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{    
    [SerializeField] private float Speed;

    [HideInInspector] public Vector3 Input;
    private bool Climbing = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.sqrMagnitude > 0.25f)
        {
            Vector3 move = new Vector3(0, Input.x, 0) * Speed * Time.fixedDeltaTime;
            transform.position += move;
        }
    }
}
