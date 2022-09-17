using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClientAnim : MonoBehaviour
{
    //public Transform Anchor;
    public Transform Head;
    //[Header("SpawnJump")]
    //public Vector3 JumpInitRot;
    //public float JumpSpeed;
    //[Header("SpawnKnockback")]
    //public Vector3 KnockbackRot;
    //public float KnockbackSpeed;
    [Header("NormalAnimation")]
    public float ItemSpawnDelay;
    public Vector2 BopRateRange;
    public Vector2 BopStrengthRange;
    private float bopRate;
    private float bopStrength;
    private bool finished;
    private Client client;

    public void Spawn(Client client)
    {
        this.client = client;
        //bopRate = Random.Range(BopRateRange.x, BopRateRange.y);
        //bopStrength = Random.Range(BopStrengthRange.x, BopStrengthRange.y);
        //Anchor.DORotate(Vector3.zero, 1 / JumpSpeed, RotateMode.WorldAxisAdd).SetEase(Ease.InCirc).OnKill(() =>
        //    Anchor.DORotate(KnockbackRot, 1 / KnockbackSpeed, RotateMode.WorldAxisAdd).SetEase(Ease.OutCirc).OnKill(() =>
        //    Anchor.DORotate(Vector3.zero, 1 / KnockbackSpeed, RotateMode.WorldAxisAdd).SetEase(Ease.InOutCirc).OnKill(() =>
        //    { BeginBop(); finished = true; })));
    }
    
    private void BeginBop()
    {
        Head.DOScaleY(1 - bopStrength / 2, 1 / bopRate).SetEase(Ease.InOutSine).OnKill(() =>
            Head.DOScaleY(1 + bopStrength / 2, 1 / bopRate).SetEase(Ease.InOutSine).OnKill(() => BeginBop()));
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
