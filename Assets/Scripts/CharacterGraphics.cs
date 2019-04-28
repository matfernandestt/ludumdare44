using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphics : MonoBehaviour
{
    public Renderer Renderer;

    private Material material;

    private float saturationAmount = 1;

    private void Start()
    {
        material = Renderer.material;
    }

    private void Update()
    {
        var ambientColor = RenderSettings.ambientLight;
        material.SetColor("_AmbientColor", ambientColor);
        material.SetFloat("_SaturationAmount", saturationAmount);
    }
}
