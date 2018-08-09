using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform ObjectToFollow;
    public Vector3 CameraAngle;

    void LateUpdate()
    {
        transform.position = ObjectToFollow.position + CameraAngle;
        transform.LookAt(ObjectToFollow);
    }

}
