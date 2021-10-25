using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData singleton;
    public Text scoreText = null;
    public GameObject musicSlider;
    public GameObject soundSlider;
    int score = 0;

    void Awake()
    {
        GameObject[] gameData = GameObject.FindGameObjectsWithTag("gameData");

        if (gameData.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        singleton = this;

        musicSlider.GetComponent<UpdateMusic>().Start();
        soundSlider.GetComponent<UpdateSound>().Start();

        PlayerPrefs.SetInt("score", 0);
    }

    public void UpdateScore(int s)
    {
        score += s;
        PlayerPrefs.SetInt("score", score);
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
