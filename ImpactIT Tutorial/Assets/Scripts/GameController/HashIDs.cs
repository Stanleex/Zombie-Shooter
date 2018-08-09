

using UnityEngine;

public class HashIDs : MonoBehaviour {

    [HideInInspector]
    public int 
            Speed, 
            IsDead,
            IsWalking,
            IsSneaking,
            IsShouting,
            IsShooting;

    void Awake()
    {
        Speed = Animator.StringToHash("Speed");
        IsDead = Animator.StringToHash("IsDead");
        IsWalking = Animator.StringToHash("IsWalking");
        IsSneaking = Animator.StringToHash("IsSneaking");
        IsShouting = Animator.StringToHash("IsShouting");
        IsShooting = Animator.StringToHash("IsShooting");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
