using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class OriginSocket : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private ObiRope Cable;
    [SerializeField] private ObiRopeCursor Cursor;
    [Space]
    [SerializeField] private Transform Socket;
    [SerializeField] public Plug Plug;
    [Space]
    [Range(0, 10)]
    [SerializeField] private float MinStrain = 1;
    [Range(0, 10)]
    [SerializeField] private float MaxStrain = 1;

    private void FixedUpdate()
    {
        //check cable tension
        float strain = Cable.CalculateLength() / Cable.restLength;
        if (strain > MaxStrain)
        {
            //if tension is too hight, spawn more cable
            IncreaseCable();
        }
        else if (strain < MinStrain)
        {
            //if tension is too low, despawn cable
            DecreaseCable();
        }
    }

    public void IncreaseCable()
    {
        Cursor.ChangeLength(Cable.restLength + Speed * Time.fixedDeltaTime);
    }

    public void DecreaseCable()
    {
        Cursor.ChangeLength(Cable.restLength - Speed * Time.fixedDeltaTime);
    }
}