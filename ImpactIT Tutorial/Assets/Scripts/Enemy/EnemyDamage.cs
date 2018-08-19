using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public float Damage = 10f;
    public float TimeBetweenAttacks = 1f;//1sec

    private GameObject _Player;
    private PlayerHealth _PlayerHealth;
    private bool _PlayerInRange;
    private float _Timer;

    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag(Tags.Player);

        if (_Player != null)
        {
            _PlayerHealth = _Player.GetComponent<PlayerHealth>();
        }
        _PlayerInRange = false;

    }

    void Update()
    {
        _Timer += Time.deltaTime;
        if (_Timer >= TimeBetweenAttacks && _PlayerInRange && _PlayerHealth.CurrentHealth > 0)
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _Player)
        {
            _PlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _PlayerInRange = false;
        }
    }


    void Attack()
    {
        _Timer = 0f;
        _PlayerHealth.LoseHealth(Damage);       
    }

}
