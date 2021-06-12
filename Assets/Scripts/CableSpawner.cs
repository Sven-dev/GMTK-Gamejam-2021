using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject jointPrefab;
    [SerializeField] private Transform parent;
    [Space]
    [SerializeField] [Range(1, 1000)] private int length = 1;
    [Space]
    [SerializeField] private float jointDistance = 0.15f;
    [Space]
    [SerializeField] private bool snapFirst;
    [SerializeField] private bool snapLast;

    // Start is called before the first frame update
    void Start()
    {
        SpawnJoint();
    }

    public void SpawnJoint()
    {
        int count = (int)(length / jointDistance);

        for (int x = 0; x < count; x++)
        {
            GameObject temp = Instantiate(
                jointPrefab, 
                new Vector3(transform.position.x,transform.position.y + jointDistance * (x - 1), transform.position.z), 
                Quaternion.identity,
                parent);

            temp.transform.eulerAngles = new Vector3(180, 0, 0);
            temp.name = parent.childCount.ToString();

            if (x == 0)
            {
                Destroy(temp.GetComponent<CharacterJoint>());

                if (snapFirst)
                {
                    temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    Destroy(temp.GetComponent<Collider>());
                }
            }
            else
            {
                temp.GetComponent<CharacterJoint>().connectedBody = parent.Find((parent.childCount -1).ToString()).GetComponent<Rigidbody>();
            }
        }

        if (snapLast)
        {
            parent.Find((parent.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
