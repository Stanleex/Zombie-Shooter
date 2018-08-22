using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [Range(0,360)]
    public float EnemyFov = 120f;
    [Range(5,30)]
    public float DetectionRange = 15f;

    private float DeadZone = 5f;
    private NavMeshAgent _NavMeshAgent;
    private GameObject _Player;
    private Animator _Animator;
    private HashIDs _hash;
    private SphereCollider _SphereCollider;
    private EnemyAnimatorHelper _AnimatorHelper;
    private bool _EnemyDeathAnimation;
    private Health _EnemyHealth;
    private CapsuleCollider _EnemyCollider;
    //fov - filed of view

    private bool EnemyCanSeePlayer;
    private bool EnemyCanHearPlayer;

    private Vector3 _LastKnownPosition;
    float speed;
    float angle;

    void Awake()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        _SphereCollider = GetComponent<SphereCollider>();
        _Player = GameObject.FindGameObjectWithTag(Tags.Player);
        _Animator = GetComponent<Animator>();
        _hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
        _EnemyHealth = GetComponent<Health>();
        _EnemyCollider = GetComponent<CapsuleCollider>();
        _AnimatorHelper = new EnemyAnimatorHelper(_Animator, _hash);

        _NavMeshAgent.updateRotation = false;

        DeadZone *= Mathf.Deg2Rad;

        _Animator.SetLayerWeight(1, 1);

        _LastKnownPosition = transform.position;
        speed = 0;
        angle = 0;

        EnemyCanSeePlayer = false;
        EnemyCanHearPlayer = false;

        _EnemyDeathAnimation = true;
        _SphereCollider.radius = DetectionRange;
    }

    void OnAnimatorMove()
    {
        _NavMeshAgent.velocity = _Animator.deltaPosition / Time.deltaTime;

        transform.rotation = _Animator.rootRotation;
    }

    void Update()
    {
        if (!_EnemyHealth.IsDead)
        {
            DetectionManager(_LastKnownPosition);
            DecisionManager();
        }
        else
        {
            DeathManager();
            _EnemyCollider.isTrigger = true;
        }
          
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _LastKnownPosition = _Player.transform.position;
            EnemyCanHearPlayer = true;
            Debug.DrawLine(transform.position + Vector3.up, _LastKnownPosition + Vector3.up, Color.green);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _LastKnownPosition = _Player.transform.position;
            EnemyCanSeePlayer = false;
            EnemyCanHearPlayer = false;
        }
    }

    private void DeathManager()
    {
        if (_EnemyDeathAnimation)
        {
            _Animator.SetTrigger(_hash.IsDead);
            _EnemyDeathAnimation = false;
        }

    }

    void StopAnimation()
    {
        _Animator.SetFloat(_hash.EnemySpeed, 0f);
        _NavMeshAgent.speed = _Animator.GetFloat(_hash.EnemySpeed);
    }

    void DetectionManager(Vector3 position)
    {
            Vector3 direction = position - transform.position;
            float signAngle = Vector3.Angle(direction, transform.forward);

            if (signAngle < EnemyFov / 2)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + Vector3.up, direction.normalized, out hit, _SphereCollider.radius))
                {
                    if (hit.collider.gameObject == _Player)
                    {
                        EnemyCanSeePlayer = true;
                        Debug.DrawLine(transform.position + Vector3.up, position + Vector3.up, Color.red);
                    }
                    else
                    {
                        EnemyCanSeePlayer = false;
                    }
                }
            }

        
    }

    void DecisionManager()
    {

        angle = FindAngle(transform.forward, _LastKnownPosition - transform.position, transform.up);
        if (Vector3.Distance(_LastKnownPosition, transform.position) < 1.3f)
        {
            if (EnemyCanHearPlayer)
            {
                MovementManager(0,angle);
            }
            MovementManager(0, 0);
            //attack
            return;
        }
        else
        {
            if (EnemyCanHearPlayer)
            {
                //turn to sound source
                speed = 1f;
            }
            if (EnemyCanSeePlayer)
            {
                //attack
                speed = 2f;
            }

            MovementManager(speed, angle);           
            return;

        }

    }

    void MovementManager(float speed = 0, float angle = 0)
    {
        if (Mathf.Abs(angle) < DeadZone)
        {
            transform.LookAt(transform.position + _NavMeshAgent.desiredVelocity);
        }
        _AnimatorHelper.Setup(speed, angle);

    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
        {
            return 0f;
        }

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);

        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));

        angle *= Mathf.Deg2Rad;

        return angle;


    }

}
