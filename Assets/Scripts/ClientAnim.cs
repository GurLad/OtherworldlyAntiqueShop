using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClientAnim : MonoBehaviour
{
    public Transform Head;
    public List<SpriteRenderer> Renderers;
    public Transform Anchor;
    // Spawn animation
    [Header("SpawnJump")]
    public Vector3 JumpInitRot;
    public float JumpSpeed;
    [Header("SpawnKnockback")]
    public Vector3 KnockbackRot;
    public float KnockbackSpeed;
    // Spawn animation end
    [Header("NormalAnimation")]
    public float ItemSpawnDelay;
    public Vector2 BopRateRange;
    public Vector2 BopStrengthRange;
    [Header("LeaveAnimation")]
    public float LeaveSpeed;
    public float LeavePos;
    private float headInitPos;
    private float bopRate;
    private float bopStrength;
    [HideInInspector]
    public bool finished;
    private Client client;

    public void Spawn(Client client)
    {
        this.client = client;
        // Spawn animation
        Anchor.localEulerAngles = JumpInitRot;
        headInitPos = Head.transform.localPosition.y;
        bopRate = Random.Range(BopRateRange.x, BopRateRange.y);
        bopStrength = Random.Range(BopStrengthRange.x, BopStrengthRange.y);
        Anchor.DORotate(-JumpInitRot, 1 / JumpSpeed, RotateMode.WorldAxisAdd).SetEase(Ease.InCirc).OnKill(() =>
            Anchor.DORotate(KnockbackRot, 1 / KnockbackSpeed, RotateMode.WorldAxisAdd).SetEase(Ease.OutCirc).OnKill(() =>
            Anchor.DORotate(-KnockbackRot, 1 / KnockbackSpeed, RotateMode.WorldAxisAdd).SetEase(Ease.InOutCirc).OnKill(() =>
            { BeginBop(); finished = true; })));
        // Spawn animation end
    }

    public void Leave()
    {
        foreach (SpriteRenderer renderer in Renderers)
        {
            renderer.DOFade(0, 1 / LeaveSpeed);
        }
        Anchor.DOLocalMoveX(LeavePos, 1 / LeaveSpeed);
    }
    
    private void BeginBop()
    {
        Head.DOLocalMoveY(headInitPos - bopStrength / 2, 1 / bopRate).SetEase(Ease.InOutSine).OnKill(() =>
            Head.DOLocalMoveY(headInitPos + bopStrength / 2, 1 / bopRate).SetEase(Ease.InOutSine).OnKill(() => BeginBop()));
    }

    private void Update()
    {
        if (finished)
        {
            ItemSpawnDelay -= Time.deltaTime;
            if (ItemSpawnDelay <= 0)
            {
                client.SpawnItem();
                finished = false;
            }
        }
    }
}
