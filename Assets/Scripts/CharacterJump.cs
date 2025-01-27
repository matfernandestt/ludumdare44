﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;

[System.Serializable]
public struct SequentialJump
{
    public string AnimationTrigger;
    public float StrenghtModifier;
}

public class CharacterJump : MonoBehaviour
{
    private Rewired.Player input;
    private Rigidbody body;
    private GameProperties gP;

    [SerializeField]
    private Transform groundCastOrigin;

    [SerializeField]
    private float groundedTestDistance = 0.1f;

    [SerializeField]
    private LayerMask groundedTestMask;

    [SerializeField]
    private float minTimeBeforeNextJump = 0.05f;

    private float timeSinceLastJump = 0f;

    private float timeSinceGrounded = 0f;

    private int sequentialJumpIndex = 0;

    private bool lastFrameGroundedState;

    private bool isHovering;

    private bool isGrounded;

    public bool IsGrounded
    {
        get
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(
                origin: groundCastOrigin.position,
                direction: Vector3.down,
                maxDistance: groundedTestDistance,
                layerMask: groundedTestMask,
                hitInfo: out hitInfo)
            )
            {
                var parenting = hitInfo.collider.GetComponentInParent<ParentingFallback>();
                if (parenting != null)
                    transform.parent = parenting.transform;
                if (isHovering)
                {
                    isHovering = false;
                    CharacterGraphics.Instance.CallJumpEnd();
                }
                return true;
            }
            transform.parent = null;
            return false;
        }
    }

    private void Start()
    {
        input = ReInput.players.GetPlayer(0);
        body = GetComponent<Rigidbody>();
        gP = GameProperties.Instance;
    }

    void Update()
    {
        UpdateTrackers();
        timeSinceLastJump += Time.deltaTime;
        Physics.gravity = new Vector3(0f,-gP.GlobalGravity,0f);
        Jump();
        if (!isHovering && !isGrounded)
        {
            isHovering = true;
            CharacterGraphics.Instance.CallFreeFall();
        }
    }

    private void UpdateTrackers()
    {
        isGrounded = IsGrounded;

        if (!lastFrameGroundedState && isGrounded)
            timeSinceGrounded = 0f;

        if (isGrounded)
            timeSinceGrounded += Time.deltaTime;

        lastFrameGroundedState = isGrounded;
    }

    private void Jump()
    {
        if (!input.GetButtonDown("Jump"))
            return;

        if (!IsGrounded)
            return;

        if (timeSinceLastJump < minTimeBeforeNextJump)
            return;

        CharacterGraphics.Instance.CallJumpAnimation(gP.PlayerSequentialJumps[sequentialJumpIndex].AnimationTrigger);

        isHovering = true;

        body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(
            Vector3.up 
            * gP.PlayerJumpStrength 
            * GetStrengthModifier(sequentialJumpIndex), 
            ForceMode.Impulse);

        if (sequentialJumpIndex == 0 || timeSinceGrounded <= gP.PlayerSequentialJumpMaxTimeGap)
            sequentialJumpIndex++;

        if (sequentialJumpIndex >= gP.PlayerSequentialJumps.Length)
            sequentialJumpIndex = 0;

        timeSinceLastJump = 0f;
        timeSinceGrounded = float.MaxValue;
    }

    private float GetStrengthModifier(int jumpIndex)
    {
        var list = gP.PlayerSequentialJumps;
        if (list.Length == 0)
            return 1f;
        if (jumpIndex >= list.Length)
            return 1f;
        return list[jumpIndex].StrenghtModifier;
    }

}
