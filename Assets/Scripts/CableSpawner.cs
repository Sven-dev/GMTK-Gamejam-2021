using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSpawner : MonoBehaviour
{
    [SerializeField] private Joint PlugPrefab;
    [SerializeField] private Joint CablePrefab;
    [Space]
    [SerializeField] private Joint SocketJoint;
    [SerializeField] private Transform CableHolder;
    [SerializeField] [Range(1, 100)] private int Length = 1;
    [Space]
    [SerializeField] private float JointDistance = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnJoints();
    }

    public void SpawnJoints()
    {
        //Calculate cable length
        int count = (int)(Length / JointDistance);

        //Spawn cable plug
        Joint connectorB = Instantiate(PlugPrefab, transform.position, Quaternion.identity, CableHolder);
        Plug plug = connectorB.GetComponent<Plug>();
        plug.CableHolder = CableHolder;

        //Spawn cables
        Joint previousJoint = null;
        for (int x = 0; x < count; x++)
        {
            Joint joint = Instantiate(
                CablePrefab,
                transform.position, 
                Quaternion.identity,
                CableHolder);

            joint.name = transform.childCount.ToString();

            Cable cableScript = joint.GetComponent<Cable>();
            if (cableScript != null)
            {
                cableScript.plugEnd = plug;
                cableScript.Length = count;
            }

            if (x == 0)
            {
                //Connect joint to socket
                SocketJoint.connectedBody = joint.GetComponent<Rigidbody>();
            }
            else
            {
                //Connect joint to previous part of cable
                previousJoint.connectedBody = joint.GetComponent<Rigidbody>();
            }

            previousJoint = joint;
        }

        //Spawn last connector (with character joint)
        connectorB.transform.Rotate(Vector3.up * 180);
        previousJoint.connectedBody = connectorB.GetComponent<Rigidbody>();
        connectorB.connectedBody = plug.SelfConnector;
    }
}