using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float Speed = 6f;
    public float SpeedDumbTime = 0.1f;



    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody _rigidBody;
    private int _groundMask;
    private float _camRayLength = 100f;
    private HashIDs _hash;


    void Awake()
    {
        _groundMask = LayerMask.GetMask(Layers.Ground);
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();

        _hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw(Inputs.Horizontal);

        float v = Input.GetAxisRaw(Inputs.Vertical);

        MovementManager(h, v);
        Turning();

    }

    void MovementManager(float horizontal, float vertical)
    {
        if (horizontal != 0f || vertical != 0f)
        {
            _animator.SetFloat(_hash.Speed, 5.6f,SpeedDumbTime,Time.deltaTime);
        }
        else
        {
            _animator.SetFloat(_hash.Speed, 0f);
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

}
