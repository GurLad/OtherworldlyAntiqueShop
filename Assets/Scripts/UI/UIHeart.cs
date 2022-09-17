using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour
{
    public ParticleSystem DeathParticles;
    public Image Image;
    public float DeathTime;

    public void Destroy()
    {
        Image.gameObject.SetActive(false);
        DeathParticles.gameObject.SetActive(true);
    }

    private void Reset()
    {
        Image = GetComponentInChildren<Image>();
        DeathParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (Image == null)
        {
            DeathTime -= Time.deltaTime;
            if (DeathTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
