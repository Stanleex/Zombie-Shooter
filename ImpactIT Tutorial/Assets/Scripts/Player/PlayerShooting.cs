using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    private Animator _animator;
    private HashIDs _hash;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();

        _animator.SetLayerWeight(2, 1);
        _animator.SetLayerWeight(3, 1);
    }

    void Update()
    {
        bool Shoot = Input.GetButton(Inputs.Fire1);
        ShootingManager(Shoot);

    }

    void ShootingManager(bool shoot)
    {
        _animator.SetBool(_hash.IsShooting, shoot);
    }
}
