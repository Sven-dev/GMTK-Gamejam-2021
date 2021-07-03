using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    [SerializeField] private LayerMask Punchables;

    public void Punch()
    {
        print("punching");

        //Check if there's something punchable in front of the character

        Ray ray = new Ray(transform.position + transform.up * 0.2f, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 5f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1, Punchables))
        {
            Punchable test = hit.transform.GetComponent<Punchable>();
            print(test.name);

            test.Punch();
        }
    }
}