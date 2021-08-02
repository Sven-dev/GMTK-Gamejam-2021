using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{    
    [SerializeField] private float Speed;
    [SerializeField] private PlayerController Controller;
    [SerializeField] private Rigidbody Rigidbody; //Uses the main player rigidbody, not the one on the climber object!

    private bool Climbing = false;
    private Vector2 InputDirection = Vector2.one * 2;
    private int Direction = 0;

    [HideInInspector] public Vector2 Input = Vector2.zero;

    public void StartClimb(Vector2 input)
    {
        //Log keys so we know what keys need to be pressed in order to move up
        InputDirection = input;
        Rigidbody.useGravity = false;
    }

    public void StopClimb()
    {
        Input = Vector2.zero;
        InputDirection = Vector2.one * 2;
        Rigidbody.useGravity = true;
    }

    void FixedUpdate()
    {
        float d = 0;
        if (Input.normalized == InputDirection)
        {
            d = Direction;
        }
        else if (Input.normalized == InputDirection * -1)
        {
            d = -Direction;
        }

        Vector3 move = new Vector3(0, d, 0) * Speed * Time.fixedDeltaTime;
        Controller.transform.position += move;
    }

    private void OnTriggerEnter(Collider other)
    {
        Climbing = !Climbing;

        print("Climbing: " + Climbing);
        if (Climbing)
        {
            Ladder ladder = other.GetComponent<Ladder>();
            Direction = ladder.Direction;
            Controller.StartClimb();

            if (Direction == -1)
            {
                StartCoroutine(_Rotate(ladder.Rotator.position));
            }
        }
        else
        {
            Controller.StopClimb();
        }
    }

    private IEnumerator _Rotate(Vector3 end)
    {
        Vector3 start = Controller.transform.position;

        float progress = 0;
        while (progress < 1)
        {
            Controller.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 180, progress), 0);
            Controller.transform.position = (Vector3.Lerp(start, end, progress));
            progress += 2f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}