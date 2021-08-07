using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private float Cooldown = 1f;
    [SerializeField] private Transform TeleportPosition;
    [Space]
    [SerializeField] private Teleporter Exit;

    [HideInInspector] public bool OnCooldown;

    private void OnTriggerEnter(Collider other)
    {
        if (!OnCooldown)
        {
            Teleport(other.transform);
        }
    }

    private void Teleport(Transform player)
    {
        StartCoroutine(_Cooldown());
        player.transform.position = Exit.TeleportPosition.position;
    }

    private IEnumerator _Cooldown()
    {
        OnCooldown = true;
        Exit.OnCooldown = true;

        yield return new WaitForSeconds(Cooldown);

        OnCooldown = false;
        Exit.OnCooldown = false;
    }
}