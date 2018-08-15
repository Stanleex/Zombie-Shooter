using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorHelper {
    public float SpeedDampTime = 0.1f;
    public float AngularSpeedDampTime = 0.7f;
    public float AngleResponseTime = 0.6f;

    private Animator _Animator;
    private HashIDs _Hash;

    public EnemyAnimatorHelper(Animator animator, HashIDs hash)
    {
        _Animator = animator;
        _Hash = hash;
    }

    public void Setup(float speed, float angle)
    {
        float angularSpeed = angle / AngleResponseTime;
        _Animator.SetFloat(_Hash.EnemySpeed, speed, SpeedDampTime,Time.deltaTime);
        _Animator.SetFloat(_Hash.EnemyAngularSpeed, angularSpeed,AngularSpeedDampTime, Time.deltaTime);

    }

}
