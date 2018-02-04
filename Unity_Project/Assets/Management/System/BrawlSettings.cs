using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlSettings : MonoBehaviour
{
    int itemRate;
    string[] names = new string[2];
    int lives;
    int time;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetItemRate(int itemRateIn)
    {
        itemRate = itemRateIn;
    }

    public int GetItemRate()
    {
        return itemRate;
    }

    public void SetNames(string name1, int nameIndex)
    {
        names[nameIndex] = name1;
    }

    public string[] GetNames()
    {
        return names;
    }

    public void SetLives(int livesIn)
    {
        lives = livesIn;
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetTime(int timeIn)
    {
        time = timeIn;
    }

    public int GetTime()
    {
        return time;
    }
}
