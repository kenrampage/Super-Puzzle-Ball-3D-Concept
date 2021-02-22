using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public static float level1Time;
    public static float level2Time;
    public static float level3Time;
    public static float level4Time;
    public static float level5Time;
    public static float totalTime;

    public GameObject levelEndMenuUI;
    public GameObject gameEndMenuUI;

    /*
    public void UpdateScoreText()
    {

        
        if (GameManager.level == 1)
        {
            level1Time = GameManager.levelTimer;
            level2Time = 0;
            level3Time = 0;
            level4Time = 0;
            level5Time = 0;
            totalTime = level1Time + level2Time + level3Time + level4Time + level5Time;
        }
        else if (GameManager.level == 2)
        {
            level2Time = GameManager.levelTimer;
            totalTime = level1Time + level2Time + level3Time + level4Time + level5Time;
        }
        else if (GameManager.level == 3)
        {
            level3Time = GameManager.levelTimer;
            totalTime = level1Time + level2Time + level3Time + level4Time + level5Time;
        }
        else if (GameManager.level == 4)
        {
            level4Time = GameManager.levelTimer;
            totalTime = level1Time + level2Time + level3Time + level4Time + level5Time;
        }
        else if (GameManager.level == 5)
        {
            level5Time = GameManager.levelTimer;
            totalTime = level1Time + level2Time + level3Time + level4Time + level5Time;
        }



    }

    */

    public void ResetTimers()
    {
        level1Time = 0;
        level2Time = 0;
        level3Time = 0;
        level4Time = 0;
        level5Time = 0;
        totalTime = 0;
    }

    public void setTimerText()
    {
        levelEndMenuUI.transform.Find("Level" + 1 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 1 + ": " + level1Time.ToString("F2") + "s";
        levelEndMenuUI.transform.Find("Level" + 2 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 2 + ": " + level2Time.ToString("F2") + "s";
        levelEndMenuUI.transform.Find("Level" + 3 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 3 + ": " + level3Time.ToString("F2") + "s";
        levelEndMenuUI.transform.Find("Level" + 4 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 4 + ": " + level4Time.ToString("F2") + "s";
        levelEndMenuUI.transform.Find("Level" + 5 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 5 + ": " + level5Time.ToString("F2") + "s";
        levelEndMenuUI.transform.Find("TotalTime").GetComponent<TextMeshProUGUI>().text = "Total: " + totalTime.ToString("F2") + "s";

        gameEndMenuUI.transform.Find("Level" + 1 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 1 + ": " + level1Time.ToString("F2") + "s";
        gameEndMenuUI.transform.Find("Level" + 2 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 2 + ": " + level2Time.ToString("F2") + "s";
        gameEndMenuUI.transform.Find("Level" + 3 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 3 + ": " + level3Time.ToString("F2") + "s";
        gameEndMenuUI.transform.Find("Level" + 4 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 4 + ": " + level4Time.ToString("F2") + "s";
        gameEndMenuUI.transform.Find("Level" + 5 + "Time").GetComponent<TextMeshProUGUI>().text = "Lvl" + 5 + ": " + level5Time.ToString("F2") + "s";
        gameEndMenuUI.transform.Find("TotalTime").GetComponent<TextMeshProUGUI>().text = "Total: " + totalTime.ToString("F2") + "s";

    }

}


