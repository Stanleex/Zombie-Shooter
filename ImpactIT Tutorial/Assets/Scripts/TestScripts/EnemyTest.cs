using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour {

    [Range(0, 5)]
    public float Speed = 0;

    [Range(0,3)]
    public int Skills = 0;

    private Animator _Anim;
    private HashIDs _Hash;

    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
    }


    void FixedUpdate()
    {
        MovementManager(Speed);
        SkillsManager(Skills);
    }

    void MovementManager(float speed)
    {
            _Anim.SetFloat(_Hash.EnemySpeed, Speed);
    }

    void SkillsManager(int skill)
    {
        _Anim.SetInteger(_Hash.Skills, skill);
    }


}
