using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public PPTScript PPTScript;
    public Material Material;
    public Color MaxPoisonColor;
    public Vector2 BlurRange;
    public float PoisonFadeSpeed;
    private float strength;

    private void Start()
    {
        PPTScript.Material = Material = Instantiate(Material);
    }

    private void Update()
    {
        if (strength > 0)
        {
            strength -= Time.unscaledDeltaTime * PoisonFadeSpeed;
            Color color = (MaxPoisonColor * strength + Color.white * (1 - strength));
            Material.SetColor("_ColorModifier", color);
            Material.SetFloat("_BlurRadius", BlurRange.x * strength + BlurRange.y * (1 - strength));
            Material.SetFloat("_BlurStrength", strength);
        }
        else
        {
            Material.SetColor("_ColorModifier", Color.white);
            Material.SetFloat("_BlurStrength", 0);
        }
    }

    public void ApplyPoison()
    {
        strength = 1;
    }
}
