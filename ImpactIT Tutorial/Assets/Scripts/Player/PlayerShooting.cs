using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    //private Animator _animator;
    //private HashIDs _hash;

    public LayerMask Mask;
    public Transform whiteRabbit;

    private Animator _Animator;
    private HashIDs _Hash;
    bool Shoot;
    void Awake()
    {
        _Animator = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Animator>();
        _Hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
        Shoot = false;
        _Animator.SetLayerWeight(2, 1);
        _Animator.SetLayerWeight(3, 1);

    }

    void Update()
    {
        Shoot = Input.GetButton(Inputs.Fire1);

        Attack(Shoot);
       
    }

    RaycastHit hit;
    void Attack(bool shoot)
    {
        _Animator.SetBool(_Hash.IsShooting, shoot);

        if (shoot && _Animator.GetFloat(_Hash.Shot)>0.9f)
        {
            Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);

            

            if (Physics.Raycast(camray, out hit, 300f, Mask))
            {
                if (whiteRabbit != null)
                {

                    whiteRabbit.position = hit.point;
                    Debug.DrawLine(transform.position, whiteRabbit.transform.position, Color.white);
                }
            }
        }


    }
}
