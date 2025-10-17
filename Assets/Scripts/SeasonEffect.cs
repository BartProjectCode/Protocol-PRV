using System;
using UnityEngine;

public class SeasonEffect : MonoBehaviour
{
    public GameObject gameManager;
    public SeasonsCycle seasonsCycle;
    public string buffSeason;
    public GameObject ownPrefab;
    public float radius = 5f;
    public float offspringNumber = 3f;
    public float maxOffspringNumber = 3f;

    private void Start()
    {
        gameManager = GameObject.Find("GM");
        seasonsCycle = gameManager.GetComponent<SeasonsCycle>();
        ownPrefab = gameObject;
        SetBuffSeason();
        offspringNumber = 0;
    }

    private void Update()
    {
        Vector3 rdPos = UnityEngine.Random.insideUnitCircle * radius;

        SetToMaxOffSpring();
        
        //Si c'est la bonne saison, fait des enfants
        if (seasonsCycle.currentSeason == buffSeason && offspringNumber > 0f)
        {
            // Debug.Log("It's my season!!!!!! ");
            rdPos.z = rdPos.y;
            rdPos.y = 0f;
            Instantiate(ownPrefab, transform.position + rdPos, Quaternion.identity);
            offspringNumber--;
        }
    }

    //Reset le compte d'enfant que l'entité peut avoir
    private void SetToMaxOffSpring()
    {
        if (seasonsCycle.currentSeason != buffSeason)
        {
            offspringNumber = maxOffspringNumber;
        }
    }

    //Indique à l'entité à quelle saison il doit se reproduire
    private void SetBuffSeason()
    {
        if (gameObject.CompareTag("Water"))
        {
            buffSeason = "Winter";
        }
        else if (gameObject.CompareTag("Fire"))
        {
            buffSeason = "Summer";
        }
        else if (gameObject.CompareTag("Earth"))
        {
            buffSeason = "Spring";
        }
        else if (gameObject.CompareTag("Wind"))
        {
            buffSeason = "Automn";
        }
    }
}
