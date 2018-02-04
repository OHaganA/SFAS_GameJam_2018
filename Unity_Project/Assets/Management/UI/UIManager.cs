using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // --------------------------------------------------------------
    private BrawlSettings brawlSettings;
    [SerializeField]
    public Text m_Player1ScoreText;

    [SerializeField]
    public Text m_Player2ScoreText;

    [SerializeField]
    Text m_Player1NameText;

    [SerializeField]
    Text m_Player2NameText;

    [SerializeField]
    Text m_TimerText;


    // --------------------------------------------------------------

    public int m_Player1Score = 1;
    public int m_Player2Score = 1;
    public float timeRemaining;

    // --------------------------------------------------------------
    private void Awake()
    {
        brawlSettings = GameObject.Find("BrawlSettings").GetComponent<BrawlSettings>();
    }
    private void Start()
    {
        string[] names = brawlSettings.GetNames();
        m_Player1NameText.text = names[0];
        m_Player2NameText.text = names[1];
        m_Player1Score = brawlSettings.GetLives();
        m_Player2Score = brawlSettings.GetLives();
        m_Player1ScoreText.text = m_Player1Score.ToString();
        m_Player2ScoreText.text = m_Player2Score.ToString();
        timeRemaining = brawlSettings.GetTime() * 60;
    }

    private void Update()
    {
        Timer();
    }
    void OnEnable()
    {
        DeathTrigger.OnPlayerDeath += OnUpdateScore;
    }

    void OnDisable()
    {
        DeathTrigger.OnPlayerDeath -= OnUpdateScore;
    }


    void OnUpdateScore(int playerNum)
    {
        if(playerNum == 1)
        {
            m_Player1Score -= 1;
            m_Player1ScoreText.text = "" + m_Player1Score;
        }
        else if(playerNum == 2)
        {
            m_Player2Score -= 1;
            m_Player2ScoreText.text = "" + m_Player2Score;
        }
    }

    void Timer()
    {
        timeRemaining -= Time.deltaTime;
        m_TimerText.text = string.Format("{0}:{1:00}",(int)timeRemaining / 60, (int) timeRemaining % 60);
    }
}
