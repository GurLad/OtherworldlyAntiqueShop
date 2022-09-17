using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string Name;

    public void Click()
    {
        Time.timeScale = 1;
        CrossfadeMusicPlayer.Instance.Play("Main0");
        SceneManager.LoadScene(Name);
    }
}
