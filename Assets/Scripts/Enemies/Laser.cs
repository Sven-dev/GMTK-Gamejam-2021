using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float Firetime = 1f;
    [SerializeField] private float Cooldown = 5f;
    [Space]
    [SerializeField] private GameObject Beam;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Loop());
    }

    private IEnumerator _Loop()
    {
        while (true)
        {
            //show charge up animation
            yield return new WaitForSeconds(Cooldown);

            Beam.SetActive(true);
            yield return new WaitForSeconds(Firetime);
            Beam.SetActive(false);
        }
    }
}