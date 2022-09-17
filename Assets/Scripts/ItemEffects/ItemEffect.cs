using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    public int Score;
    public bool Corrupted;

    protected abstract void BuyEffect();
    public void Buy()
    {
        // TBA: Modify score and stuff
    }
}
