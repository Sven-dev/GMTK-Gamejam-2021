using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSpawner : MonoBehaviour
{
    [SerializeField] private Joint plugPrefab;
    [SerializeField] private Joint jointPrefab;
    [Space]
    [SerializeField] private Joint socket;
    [SerializeField] private Transform parent;
    [Space]
    [SerializeField] [Range(1, 10)] private int length = 1;
    [Space]
    [SerializeField] private float jointDistance = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnJoints();
    }

    public void SpawnJoints()
    {
        //Calculate cable length
        int count = (int)(length / jointDistance);

        //Spawn cable plug
        Joint connectorB = Instantiate(plugPrefab, transform.position + (Vector3.up * jointDistance), Quaternion.identity, parent);
        Plug plug = connectorB.GetComponent<Plug>();
        plug.CableHolder = transform;

        //Spawn cables
        Joint previousJoint = socket;
        for (int x = 0; x < count; x++)
        {
            Joint joint = Instantiate(
                jointPrefab,
                transform.position + (Vector3.up * jointDistance), 
                Quaternion.identity,
                parent);

            joint.name = parent.childCount.ToString();

            Rigidbody rb = joint.GetComponent<Rigidbody>();
            previousJoint.connectedBody = rb;

            Cable cableScript = joint.GetComponent<Cable>();
            if (cableScript != null)
            {
                cableScript.plugEnd = plug;
                cableScript.Length = count;
            }

            if (x == 0)
            {
                joint.transform.Rotate(Vector3.left * 90);
                joint.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            previousJoint = joint.GetComponent<Joint>();
        }

        //Spawn last connector (with character joint)
        connectorB.transform.Rotate(Vector3.up * 180);
        previousJoint.connectedBody = connectorB.GetComponent<Rigidbody>();
        connectorB.connectedBody = plug.SelfConnector;
    }
}
