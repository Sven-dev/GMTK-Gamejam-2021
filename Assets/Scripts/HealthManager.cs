using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    [Space]
    [SerializeField] private Collider Collider;
    [SerializeField] private GameObject Model;
    [Space]
    public UnityFloatEvent OnHealthChange;

    private int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(CurrentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("collided with: " + other.tag);
        if (other.CompareTag("DamageDealer"))
        {
            LoseHealth(1);
        }
    }

    public void LoseHealth(int value)
    {
        CurrentHealth -= value;
        OnHealthChange?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            //Die
            //Play any death effects
            //Reload scene
            //Lives?
            print("Die");
        }
        else
        {
            StartCoroutine(_Iframes());
        }
    }

    public void GainHealth(int value)
    {
        CurrentHealth += value;
        OnHealthChange?.Invoke(CurrentHealth);
    }

    private IEnumerator _Iframes()
    {
        Collider.enabled = false;

        float progress = 0;
        while (progress < 1)
        {
            Model.SetActive(!Model.activeSelf);

            progress += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        Model.SetActive(true);
        Collider.enabled = true;
    }
}