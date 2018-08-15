using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {


    private NavMeshAgent _NavMeshAgent;
    private GameObject _Player;
    private Animator _Animator;
    private HashIDs _hash;
    private SphereCollider _SphereCollider;


    void Awake()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        _SphereCollider = GetComponent<SphereCollider>();
        _Player = GameObject.FindGameObjectWithTag(Tags.Player);
        _Animator = GetComponent<Animator>();
        _hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
    }

    void Update()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _Player)
        {
            MovementManager();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _Player)
        {
            StopAnimation();
        }
    }


    void StopAnimation()
    {
        _Animator.SetFloat(_hash.EnemySpeed, 0f);
        _NavMeshAgent.speed = _Animator.GetFloat(_hash.EnemySpeed);
    }

    void MovementManager()
    {
        _Animator.SetFloat(_hash.EnemySpeed, 0.2f);
        _NavMeshAgent.speed = _Animator.GetFloat(_hash.EnemySpeed);
        _NavMeshAgent.SetDestination(_Player.GetComponent<Transform>().position);
    }

}
