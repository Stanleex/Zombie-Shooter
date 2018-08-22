using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float Speed = 6f;
    public float SpeedDumbTime = 0.1f;
    public AudioClip ShoutAudioSource;


    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody _rigidBody;
    private int _groundMask;
    private float _camRayLength = 100f;
    private HashIDs _hash;

    private Health _PlayerHealth;
    private bool _PlayerDeathAnimation;

    

    void Awake()
    {
        _groundMask = LayerMask.GetMask(Layers.Ground);
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();

        _hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
        _animator.SetLayerWeight(1, 1f);

        _PlayerHealth = GetComponent<Health>();
        _PlayerDeathAnimation = true;
    }

    void Update()
    {
        if (!_PlayerHealth.IsDead)
        {
            float h = Input.GetAxisRaw(Inputs.Horizontal);
            float v = Input.GetAxisRaw(Inputs.Vertical);

            bool walk = Input.GetButton(Inputs.Walking);
            bool sneak = Input.GetButton(Inputs.Sneaking);
            bool shout = Input.GetButtonDown(Inputs.Shouting);

            MovementManager(h, v, walk, sneak);
            ActionManager(shout);
        }
        else
        {
            DeathManager();
        }

    }


    void FixedUpdate()
    {
        if (!_PlayerHealth.IsDead)
        {
            Turning();
        }
    }

    private void DeathManager()
    {
        if (_PlayerDeathAnimation)
        {
            _animator.SetTrigger(_hash.IsDead);
            _PlayerDeathAnimation = false;
        }
        
    }

    void MovementManager(float horizontal, float vertical, bool walking, bool sneaking)
    {
        if (horizontal != 0f || vertical != 0f)
        {
            _animator.SetBool(_hash.IsSneaking,sneaking);
            if (!walking)
            {
                _animator.SetFloat(_hash.Speed, 5.6f, SpeedDumbTime, Time.deltaTime);
            }
            else
            {
                _animator.SetFloat(_hash.Speed, 2.6f, SpeedDumbTime, Time.deltaTime);
            }
            
        }
        else
        {
            _animator.SetFloat(_hash.Speed, 0f, SpeedDumbTime, Time.deltaTime);
        }
    }

    void Turning()
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit groundHit;

        if (Physics.Raycast(camray, out groundHit, _camRayLength, _groundMask))
        {
            Vector3 playerToMouse = groundHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _rigidBody.MoveRotation(newRotation);

        }

    }
    void ActionManager(bool shouting)
    {

        _animator.SetBool(_hash.IsShouting, shouting);
        ShoutAudio(shouting);        
    }
    void ShoutAudio(bool shouting)
    {
        if (ShoutAudioSource != null)
        {
            if (shouting)
            {
                AudioSource.PlayClipAtPoint(ShoutAudioSource, transform.position);
            }           
        }
    }

}
