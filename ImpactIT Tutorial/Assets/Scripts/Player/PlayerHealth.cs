using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float Health = 100f;
    public float CurrentHealth;

    public bool IsDead;

    void Awake()
    {
        IsDead = false;
        CurrentHealth = Health;

    }

    void Update()
    {
        if (CurrentHealth <= 0f)
        {
            IsDead = true;
        }
    }
    
    public void LoseHealth(float value)
    {
        if (!IsDead)
        {
            CurrentHealth -= value;
        }
    }
    
}
