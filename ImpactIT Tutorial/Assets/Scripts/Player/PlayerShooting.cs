using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    //private Animator _animator;
    //private HashIDs _hash;

    public LayerMask Mask;
    public Transform whiteRabbit;
    public float WeaponDamage = 10f;

    private Animator _Animator;
    private HashIDs _Hash;
    private bool _Shoot;

    private AudioSource _LaserGunSound;
    void Awake()
    {
        _Animator = GetComponentInParent<Animator>();
        _Hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
        _LaserGunSound = GetComponent<AudioSource>();
        _Shoot = false;

        _Animator.SetLayerWeight(2, 1);
        _Animator.SetLayerWeight(3, 1);

    }

    void Update()
    {
        _Shoot = Input.GetButton(Inputs.Fire1);

        Attack(_Shoot);
       
    }

    RaycastHit hit;
    void Attack(bool shoot)
    {
        _Animator.SetBool(_Hash.IsShooting, shoot);

        if (shoot && _Animator.GetFloat(_Hash.Shot)>0.9f)
        {
            Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);


            _LaserGunSound.pitch = Random.Range(0.8f,1.2f);
            _LaserGunSound.PlayOneShot(_LaserGunSound.clip);

            if (Physics.Raycast(camray, out hit, 300f, Mask))
            {
                Health enemyHealth = hit.collider.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.LoseHealth(WeaponDamage);
                }


                if (whiteRabbit != null)
                {
                    
                    whiteRabbit.position = hit.point;
                    Debug.DrawLine(transform.position, whiteRabbit.transform.position, Color.white);
                }
            }
        }


    }
}
