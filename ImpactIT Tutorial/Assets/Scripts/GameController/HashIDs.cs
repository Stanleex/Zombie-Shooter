

using UnityEngine;

public class HashIDs : MonoBehaviour {

    [HideInInspector]
    public int 
            Speed, 
            IsDead;

    void Awake()
    {
        Speed = Animator.StringToHash("Speed");
        IsDead = Animator.StringToHash("IsDead");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
