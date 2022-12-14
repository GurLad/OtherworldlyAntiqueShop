using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemAnim : MonoBehaviour
{
    public List<Sprite> Sprites;
    public SpriteRenderer Renderer;
    [Header("SpawnJump")]
    public Vector3 JumpInitPos; // Final is Vector3.zero
    public Vector3 JumpInitSize;
    public float JumpInitFade;
    public float JumpPower;
    public float JumpSpeed;
    [Header("SpawnSquash")]
    public Transform SquashAnchor;
    public float SquashHeight;
    public float SquashSpeed;
   // [HideInInspector]
    public bool Finished;
    private Vector3 targetPos;

    private void Reset()
    {
        Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Spawn()
    {
        Renderer.sprite = Sprites[Random.Range(0, Sprites.Count)];
        Finished = false;
        targetPos = SquashAnchor.localPosition;
        SquashAnchor.localPosition = JumpInitPos;
        SquashAnchor.localScale = JumpInitSize;
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, JumpInitFade);
        Renderer.DOFade(1, 1 / JumpSpeed);


        SquashAnchor.DOScale(Vector3.one, 1 / JumpSpeed);
        SquashAnchor.DOLocalJump(targetPos, JumpPower, 1, 1 / JumpSpeed).SetEase(Ease.InSine).OnKill(() =>
            {
                Debug.Log("yoo");
                Finished = true;
                    SquashAnchor.DOScaleY(SquashHeight, 1 / SquashSpeed).OnKill(() =>
                    SquashAnchor.DOScaleY(1, 1 / SquashSpeed).OnKill(() => BeginBop()));
            });
    }

    private void BeginBop()
    {
        SquashAnchor.DOScaleY(0.9f, SquashSpeed).OnKill(() =>
            SquashAnchor.DOScaleY(1.1f, SquashSpeed).OnKill(() => BeginBop()));
    }
}
