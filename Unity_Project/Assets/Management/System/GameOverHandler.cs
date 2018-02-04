using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour {

    private UIManager uiManager;
    private LevelManager levelManager;
	// Use this for initialization
	void Start ()
    {
		uiManager = GameObject.Find("StageCanvas").GetComponent<UIManager>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(uiManager.m_Player1Score <= 0 || uiManager.m_Player2Score <= 0 || uiManager.timeRemaining <= 0)
        {
            if (uiManager.m_Player1Score > uiManager.m_Player2Score)
            {
                uiManager.m_Player1ScoreText.text = "WIN";
                uiManager.m_Player2ScoreText.text = "LOSE";
            }
            else if (uiManager.m_Player1Score < uiManager.m_Player2Score)
            {
                uiManager.m_Player2ScoreText.text = "WIN";
                uiManager.m_Player1ScoreText.text = "LOSE";
            }
            else
            {
                uiManager.m_Player1ScoreText.text = "TIE";
                uiManager.m_Player2ScoreText.text = "TIE";
            }
            StartCoroutine(QuitWait());
            if (GameObject.Find("Player_1"))
            {
                GameObject.Find("Player_1").SetActive(false);
                GameObject.Find("Player_2").SetActive(false);
                GameObject.Find("Canvas/Timer").SetActive(false);
            }
        }
    }

    IEnumerator QuitWait()
    {
        yield return new WaitForSeconds(3f);
        levelManager.LoadLevel("MainMenu");
}
}
