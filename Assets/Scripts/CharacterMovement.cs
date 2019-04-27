using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour
{
    private Rewired.Player input;
    private GameProperties gP;
    private Rigidbody body;
    private Transform cameraTransform;

    private Vector3 moveStep;
    private Quaternion targetRotation;

    private void Start()
    {
        gP = GameProperties.Instance;
        input = ReInput.players.GetPlayer(0);
        body = GetComponent<Rigidbody>();
        cameraTransform = FindObjectOfType<CameraController>().transform;
    }

    private void Update()
    {
        var moveInput = new Vector2(input.GetAxisRaw("MoveHorizontal"), input.GetAxisRaw("MoveVertical"));

        moveStep = cameraTransform.forward * moveInput.y;
        moveStep += cameraTransform.right * moveInput.x;
        moveStep.y = 0f;
        UpdateOrientation();

        moveStep.Normalize();

        Jump();
    }

    private void UpdateOrientation()
    {
        if (moveStep.sqrMagnitude > 0.5f)
            targetRotation = Quaternion.LookRotation(moveStep, Vector3.up);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            gP.PlayerRotationLerpFactor);
    }

    private void FixedUpdate()
    {
        var velocity = moveStep * gP.PlayerMoveSpeed;
        velocity.y = body.velocity.y;
        body.velocity = velocity;
    }

    private void Jump()
    {
        if (!input.GetButtonDown("Jump"))
            return;
        body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(Vector3.up * gP.PlayerJumpStrength, ForceMode.Impulse);
    }
}
