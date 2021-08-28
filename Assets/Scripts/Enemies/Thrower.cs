using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float Cooldown = 5f;
    [SerializeField] private LayerMask RaycastMask;
    [Space]
    [SerializeField] private ThrowerProjectile ProjectilePrefab;
    [SerializeField] private Transform ProjectileSpawn;

    private bool Throwing = false;
    private Transform Player;

    private IEnumerator _Throw()
    {
        while (Throwing)
        {
            float cooldown = Cooldown;
            while (cooldown >= 0)
            {
                transform.LookAt(Player);
                cooldown -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            print("raycast");
            Debug.DrawRay(transform.position, transform.forward * 100, Color.yellow, Cooldown);
            
            //raycast to player
            Ray Ray = new Ray(transform.position, transform.forward);
            RaycastHit RaycastHit = new RaycastHit();
            if (Physics.Raycast(Ray, out RaycastHit, 100, RaycastMask))
            {
                if(RaycastHit.transform.tag == "Player")
                {
                    //instantiate projectile
                    ThrowerProjectile projectile = Instantiate(ProjectilePrefab, ProjectileSpawn.position, transform.localRotation);
                    projectile.Distance = Vector3.Distance(transform.position, Player.position);

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Throwing = true;
        Player = other.transform;

        StartCoroutine(_Throw());
    }

    private void OnTriggerExit(Collider other)
    {
        Throwing = false;
        Player = null;

        StopCoroutine(_Throw());
    }
}