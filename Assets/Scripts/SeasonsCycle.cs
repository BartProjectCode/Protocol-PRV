using System;
using UnityEngine;

public class SeasonsCycle : MonoBehaviour
{
    public string[] seasons = new string[4];
    public string currentSeason;
    public int seasonCount;
    public float maxTimer = 100f;
    public float timer = 0f;

    private void Start()
    {
        timer = maxTimer;
        currentSeason = seasons[seasonCount];
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = maxTimer;
            seasonCount++;
            currentSeason = seasons[seasonCount];
            Debug.Log(currentSeason);
        }

        if (seasonCount >= 3)
        {
            seasonCount = 0;
        }
        // Debug.Log(timer);
    }
}
