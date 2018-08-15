

using UnityEngine;

public class HashIDs : MonoBehaviour {

    [HideInInspector]
    public int 
            Speed, 
            IsDead,
            IsWalking,
            IsSneaking,
            IsShouting,
            IsShooting,
            EnemyWalking,
            EnemySpeed,
            Skills;

    void Awake()
    {
        Speed = Animator.StringToHash("Speed");
        IsDead = Animator.StringToHash("IsDead");
        IsWalking = Animator.StringToHash("IsWalking");
        IsSneaking = Animator.StringToHash("IsSneaking");
        IsShouting = Animator.StringToHash("IsShouting");
        IsShooting = Animator.StringToHash("IsShooting");
        EnemySpeed = Animator.StringToHash("EnemySpeed");
        EnemyWalking = Animator.StringToHash("EnemyWalking");
        Skills = Animator.StringToHash("Skills");
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
