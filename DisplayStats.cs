using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    public Text lastScore;
    public Text highScore;

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("lastscore"))
        {
            lastScore.text = "Last Score: " + PlayerPrefs.GetInt("lastscore");
        }
        else
        {
            lastScore.text = "Last Score: 0";
        }

        if (PlayerPrefs.HasKey("score"))
        {
            highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore");
        }
        else
        {
            highScore.text = "High Score: 0";
        }
    }
}
