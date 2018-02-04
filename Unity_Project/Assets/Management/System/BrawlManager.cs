using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrawlManager : MonoBehaviour {

    public Slider livesSlider, timeSlider;
    public Text lives, time;
    public InputField player1, player2;
    public Image stage;

    public Sprite[] stageImages;
    public string[] stages;

    private int stageIndex;
    private BrawlSettings brawlSettings;
    private LevelManager levelManager;

    private void Start()
    {
        brawlSettings = GameObject.Find("BrawlSettings").GetComponent<BrawlSettings>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }
    void Update () {
        lives.text = livesSlider.value.ToString("0");
        time.text = timeSlider.value.ToString("0") + ":00";
        stage.sprite = stageImages[stageIndex];
    }

    void StageRight()
    {
        if(stageIndex == 5)
        {
            stageIndex = 0;
        }

        else
        {
            stageIndex++;
        }
    }

    void StageLeft()
    {
        if (stageIndex == 0)
        {
            stageIndex = 5;
        }

        else
        {
            stageIndex--;
        }
    }
    public void InfoAndLoad()
    {
        brawlSettings.SetLives((int)livesSlider.value);
        brawlSettings.SetTime((int)timeSlider.value);
        brawlSettings.SetNames(player1.text, 0);
        brawlSettings.SetNames(player2.text, 1);
        levelManager.LoadLevel(stages[stageIndex]);
    }
}
