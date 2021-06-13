using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSpawner : MonoBehaviour
{
    [SerializeField] private CharacterJoint plugPrefab;
    [SerializeField] private CharacterJoint jointPrefab;
    [Space]
    [SerializeField] private CharacterJoint socket;
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
        CharacterJoint connectorB = Instantiate(plugPrefab, transform.position + (Vector3.up * jointDistance * (count + 0.5f)), Quaternion.identity, parent);
        Plug plug = connectorB.GetComponent<Plug>();

        //Spawn cables
        CharacterJoint previousJoint = socket;
        for (int x = 0; x < count; x++)
        {
            CharacterJoint joint = Instantiate(
                jointPrefab,
                transform.position + (Vector3.up * jointDistance * (x + 1)), 
                Quaternion.identity,
                parent);

            joint.name = parent.childCount.ToString();
            previousJoint.connectedBody = joint.GetComponent<Rigidbody>();

            Cable cableScript = joint.GetComponent<Cable>();
            if (cableScript != null)
            {
                cableScript.plugEnd = plug;
                cableScript.Length = count;
            }

            previousJoint = joint.GetComponent<CharacterJoint>();
        }

        //Spawn last connector (with character joint)
        connectorB.transform.Rotate(Vector3.up * 180);
        previousJoint.connectedBody = connectorB.GetComponent<Rigidbody>();
        connectorB.connectedBody = plug.SelfConnector;
    }
}
