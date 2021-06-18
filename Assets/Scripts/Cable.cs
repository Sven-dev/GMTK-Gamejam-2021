using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Cable : MonoBehaviour
{
    [SerializeField] private ObiSolver Solver;
    [SerializeField] private Plug Plug;
    [SerializeField] private ObiColliderWorld Collider;

    [HideInInspector] public bool Trippable = false;

    private void Awake()
    {
        Collider = ObiColliderWorld.GetInstance();
    }

    private void OnEnable()
    {
        Solver.OnCollision += Collision;
    }

    private void OnDisable()
    {
        Solver.OnCollision -= Collision;
    }

    private void Collision(object sender, ObiSolver.ObiCollisionEventArgs e)
    {
        if (Trippable)
        {
            foreach (Oni.Contact contact in e.contacts)
            {
                if (contact.distance < 0.01)
                {
                    ObiColliderBase col = Collider.colliderHandles[contact.bodyB].owner;
                    if (col != null && col.CompareTag("Player"))
                    {
                        Plug.transform.parent = Plug.Source.transform;
                        Plug.transform.eulerAngles = Vector3.zero;
                        Plug.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                        Plug.PullOut();
                        break;
                    }
                }
            }
        }
    }

    public void DisableTripping()
    {
        StartCoroutine(_Timer(2));
    }

    private IEnumerator _Timer(float time)
    {
        Trippable = false;
        yield return new WaitForSeconds(time);
        Trippable = true;
    }
}