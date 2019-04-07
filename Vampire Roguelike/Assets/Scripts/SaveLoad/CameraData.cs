using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraData
{
    public string cameraTrackTarget;
    public float moveSpeed;
    public float cameraZDepth;

    public CameraData(CameraMovement camera, GameObject trackTarget)
    {
        cameraTrackTarget = trackTarget.tag;
        moveSpeed = camera.moveSpeed;
        cameraZDepth = camera.gameObject.GetComponent<Camera>().depth;
    }
}
