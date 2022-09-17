using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("ExternalObjects")]
    public CameraEffects CameraEffects;
    [Header("Objects")]
    public List<Text> ScoreDisplays;
    public UIHeart BaseLifeImage;
    public GameObject GameOverDisplay;
    public Text GameOverScore;
    public Text GameOverHighScore;
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
            lifeIcon.DeathParticles.transform.parent = null;
            lives[i] = lifeIcon;
        }
    }

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        foreach (Text scoreDisplay in ScoreDisplays)
        {
            scoreDisplay.text = "Score: " + currentScore;
        }
    }

    public void LoseLife()
    {
        lives[--currentLives].Destroy();
        CameraEffects.ApplyPoison();
        if (currentLives <= 0)
        {
            Time.timeScale = 0;
            GameOverDisplay.SetActive(true);
            if (PlayerPrefs.GetFloat("HighScore") < currentScore)
            {
                PlayerPrefs.SetFloat("HighScore", currentScore);
            }
            GameOverScore.text = "Score: " + currentScore;
            GameOverHighScore.text = "High score: " + PlayerPrefs.GetFloat("HighScore");
        }
    }
}
