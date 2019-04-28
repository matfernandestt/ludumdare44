using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterGraphics : MonoBehaviour
{
    public Renderer Renderer;

    public Animator Animator;

    private Material material;

    private float saturationAmount = 1;

    private CharacterMovement characterMovement;

    public static CharacterGraphics Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!Application.isPlaying)
            return;
        material = Renderer.material;
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        SendValuesToMaterial();
        UpdateAnimatorValues();
    }

    private void UpdateAnimatorValues()
    {
        if (!Application.isPlaying) return;
        Animator.SetFloat("MoveSpeed", characterMovement.AccelerationFactor);
    }

    private void SendValuesToMaterial()
    {
        if (!Application.isPlaying && material == null)
        {
            material = Renderer.sharedMaterial;
        }
        var ambientColor = RenderSettings.ambientLight;
        material.SetColor("_AmbientColor", ambientColor);
        material.SetFloat("_SaturationAmount", saturationAmount);
    }

    public void CallJumpAnimation(string jumpSequenceName)
    {
        Animator.SetTrigger(jumpSequenceName);
    }

    public void CallJumpEnd()
    {
        Animator.SetTrigger("JumpEnd");
    }

    public void CallFreeFall()
    {
        Animator.SetTrigger("FreeFall");
    }

    public void CallCollectItemAnimation()
    {
        Animator.SetTrigger("CollectItem");
    }

    public void CallDeathAnimation()
    {
        Animator.SetTrigger("Die");
    }
}
