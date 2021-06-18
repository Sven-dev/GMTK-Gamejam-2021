using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] private Joint CablePrefab;
    [Space]
    [SerializeField] private Joint SocketJoint;
    [SerializeField] private Transform CableHolder;
    [SerializeField] public Plug Plug;
    [Space]
    [SerializeField] [Range(1, 100)] private int Length = 1;
    [SerializeField] private float JointDistance = 0.2f;

    private List<Joint> Joints = new List<Joint>();

    private void Start()
    {
        //SpawnCable(Length);

        SocketJoint.connectedBody = Plug.Rigidbody;
    }

    private void Temp()
    {
        Joint joint = Instantiate(CablePrefab, transform.position, Quaternion.identity, CableHolder);
        SocketJoint.connectedBody = joint.GetComponent<Rigidbody>();
        if (Joints.Count == 0)
        {                      
            joint.connectedBody = Plug.Rigidbody;
        }
        else
        {
            joint.connectedBody = Joints[Joints.Count -1].GetComponent<Rigidbody>();
        }

        Joints.Add(joint);
    }

    private void Temp2()
    {
        if (Joints.Count > 5)
        {
            Joints.Remove(Joints[Joints.Count - 1]);
            Destroy(Joints[Joints.Count - 1].gameObject);

            SocketJoint.connectedBody = Joints[Joints.Count - 1].GetComponent<Rigidbody>();

        }
    }

    private void FixedUpdate()
    {
        Vector3 force = SocketJoint.currentTorque;
        force.x = 0;
        force.y = 0;
        float f = Vector3.SqrMagnitude(force);

        print("Raw torque: " + SocketJoint.currentTorque);
        print("SqurMagnitude torque: " + Vector3.SqrMagnitude(force));

        if (f > 10f)
        {
            //print("Torque: " + f + ", spawning cable");
            Temp();
        }
        else if (f == 0)
        {
            //Temp2();
        }

        //check cable tension
        //if tension is too hight, spawn more cable if available
        //if tension is too low, despawn cable if available
    }

    public void SpawnCable(int amount)
    {
        //Calculate cable length
        int count = (int)(amount / JointDistance);

        //Spawn cables
        for (int x = 0; x < count; x++)
        {
            Joint joint = Instantiate(CablePrefab, transform.position, Quaternion.identity, CableHolder);
            joint.name = CableHolder.childCount.ToString();

            if (Joints.Count == 0)
            {
                //Connect joint to socket
                SocketJoint.connectedBody = joint.GetComponent<Rigidbody>();
            }
            else
            {
                //Connect joint to previous part of cable
                Joints[Joints.Count - 1].connectedBody = joint.GetComponent<Rigidbody>();
            }

            Joints.Add(joint);
        }

        //Attach the final part of the cable to the plug
        Joints[Joints.Count - 1].connectedBody = Plug.Rigidbody;
    }
}