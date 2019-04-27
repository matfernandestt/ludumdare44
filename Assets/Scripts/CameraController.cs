using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CameraController : MonoBehaviour
{
    public Transform Target;

    private Vector3 eulerRotation;

    private Rewired.Player input;

    private GameProperties gP;

    private void Start()
    {
        gP = GameProperties.Instance;
        transform.eulerAngles = Vector3.zero;
        input = ReInput.players.Players[0];
    }

    private void LateUpdate()
    {
        if (Target == null) return;

        var lookInput = new Vector2(input.GetAxis("LookHorizontal"), input.GetAxis("LookVertical"));

        eulerRotation.x -= lookInput.y * gP.MouseSensibility * Time.deltaTime;
        eulerRotation.y += lookInput.x * gP.MouseSensibility * Time.deltaTime;
        eulerRotation.x = Mathf.Clamp(eulerRotation.x, gP.CameraPitchAxisRange.x, gP.CameraPitchAxisRange.y);

        transform.position = Target.position;
        transform.eulerAngles = eulerRotation;

        transform.position -= transform.forward * gP.CameraDistance;

        transform.position += transform.forward * gP.CameraRelativeOffset.z;
        transform.position += transform.right * gP.CameraRelativeOffset.x;
        transform.position += transform.up * gP.CameraRelativeOffset.y;
    }
}