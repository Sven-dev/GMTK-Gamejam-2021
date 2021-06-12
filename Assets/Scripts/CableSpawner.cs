using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody socket;
    [SerializeField] private CharacterJoint connectorPrefab;
    [SerializeField] private CharacterJoint jointPrefab;
    [SerializeField] private Transform parent;
    [Space]
    [SerializeField] [Range(1, 100)] private int length = 1;
    [Space]
    [SerializeField] private float jointDistance = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnJoints();
    }

    public void SpawnJoints()
    {
        int count = (int)(length / jointDistance);

        //Spawn cables
        Rigidbody previousJoint = null;
        for (int x = 0; x < count; x++)
        {
            CharacterJoint joint = Instantiate(
                jointPrefab,
                transform.position + (Vector3.up * jointDistance * (x + 1)), 
                Quaternion.identity,
                parent);

            joint.transform.eulerAngles = new Vector3(180, 0, 0);
            joint.name = parent.childCount.ToString();

            if (x == 0)
            {
                joint.connectedBody = socket;
            }
            else
            {
                joint.connectedBody = previousJoint;
            }

            previousJoint = joint.GetComponent<Rigidbody>();
        }

        //Spawn last connector (with character joint)
        CharacterJoint connectorB = Instantiate(connectorPrefab, transform.position + (Vector3.up * jointDistance * (count +1)), Quaternion.identity, parent);
        connectorB.transform.Rotate(Vector3.up * 180);
        connectorB.connectedBody = previousJoint;
    }
}
