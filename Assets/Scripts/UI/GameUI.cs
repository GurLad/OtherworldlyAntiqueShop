using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Objects")]
    public Text ScoreDisplay;
    public UIHeart BaseLifeImage;
    [Header("ObjectsData")]
    public float HeartOffset;
    [Header("Values")]
    public int BaseLifeAmount;
    private UIHeart[] lives;
    private int currentLives;
    private int currentScore;

    private void Start()
    {
        currentLives = BaseLifeAmount;
        lives = new UIHeart[BaseLifeAmount];
        for (int i = 0; i < BaseLifeAmount; i++)
        {
            UIHeart lifeIcon = Instantiate(BaseLifeImage, BaseLifeImage.transform.parent);
            lifeIcon.Image.rectTransform.anchoredPosition -= new Vector2(HeartOffset * i, 0);
            lifeIcon.gameObject.SetActive(true);
            lives[i] = lifeIcon;
        }
    }

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        ScoreDisplay.text = "Score: " + currentScore;
    }

    public void LoseLife()
    {
        lives[currentLives - 1].Destroy();
    }
}
