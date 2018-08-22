using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public float Damage = 10f;
    public float TimeBetweenAttacks = 1f;//1sec

    private GameObject _Player;
    private Health _PlayerHealth;
    private bool _PlayerInRange;
    private float _Timer;
    private Health _EnemyHealth;

    private AudioSource _EnemyAttackSound;

    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag(Tags.Player);
        _EnemyHealth = GetComponentInParent<Health>();
        _EnemyAttackSound = GetComponent<AudioSource>();


        if (_Player != null)
        {
            _PlayerHealth = _Player.GetComponent<Health>();
        }
        _PlayerInRange = false;

    }

    void Update()
    {
        if (!_EnemyHealth.IsDead)
        {
            _Timer += Time.deltaTime;
            if (_Timer >= TimeBetweenAttacks && _PlayerInRange && _PlayerHealth.CurrentHealth > 0)
            {
                Attack();
            }
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

        _EnemyAttackSound.pitch = Random.Range(0.8f, 1.2f);
        _EnemyAttackSound.PlayOneShot(_EnemyAttackSound.clip);

        _PlayerHealth.LoseHealth(Damage);
            
    }

}
