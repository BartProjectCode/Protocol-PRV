using System;
using UnityEngine;

public class SeasonsCycle : MonoBehaviour
{
    public string[] seasons = new string[4];
    public string currentSeason;
    public int seasonCount;
    public float maxTimer = 100f;
    public float timer = 0f;
    public int maxOffspring = 1;
    // public GameObject ground;
    // public Color groundMat;

    private void Start()
    {
        // ground = GameObject.FindGameObjectWithTag("Ground");
        timer = maxTimer;
        currentSeason = seasons[seasonCount];
        // groundMat = ground.GetComponent<Material>().color;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        // if (seasonCount > 3)
        // {
        //     seasonCount = 0;
        // }

        if (timer <= 0 && seasonCount < 3)
        {
            timer = maxTimer;
            seasonCount++;
            currentSeason = seasons[seasonCount];
            Debug.Log("current season is : " + currentSeason);
        }
        else if (timer <= 0 && seasonCount >= 3)
        {
            timer = maxTimer;
            seasonCount = 0;
            currentSeason = seasons[seasonCount];
            Debug.Log("current season is : " + currentSeason);

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentSeason = "Spring";
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentSeason = "Summer";
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSeason = "Automn";
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentSeason = "Winter";
        }

        // if (currentSeason == "Spring")
        // {
        //     groundMat = Color.green;
        // }
        //
        // if (currentSeason == "Summer")
        // {
        //     groundMat = Color.red;
        // }
        //
        // if (currentSeason == "Automn")
        // {
        //     groundMat = Color.yellow;
        // }
        //
        // if (currentSeason == "Winter")
        // {
        //     groundMat = Color.blue;
        // }

        
        // Debug.Log(timer);
        // Debug.Log(seasonCount);
        
    }
}
