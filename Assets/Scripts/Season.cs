using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Season : MonoBehaviour
{
    enum SeasonEnum
    {
        WINTER,
        SPRING,
        SUMMER,
        AUTOMN
    }

    [Header("Season Renderer")]
    [SerializeField] private SpriteRenderer seasonRenderer;
    [SerializeField] private Sprite springSprite;
    [SerializeField] private Sprite automnSprite;
    [SerializeField] private Sprite summerSprite;
    [SerializeField] private Sprite winterSprite;

    [Header("Season Goal")]
    [SerializeField] private List<Plant> springGoal;
    [SerializeField] private List<Plant> automnGoal;
    [SerializeField] private List<Plant> summerGoal;
    [SerializeField] private List<Plant> winterGoal;

    static public Season Instance { get; private set; }

    private SeasonEnum currentSeason;
    private List<Plant> currentGoal;

    private void Awake()
    {
        Instance = this;
    }

    private void SetSeason(SeasonEnum season)
    {
        currentSeason = season;
        
        switch (season)
        {
            case SeasonEnum.SUMMER:
                currentGoal = summerGoal;
                break;
            case SeasonEnum.SPRING:
                currentGoal = springGoal;
                break;
            case SeasonEnum.AUTOMN:
                currentGoal = automnGoal;
                break;
            case SeasonEnum.WINTER: 
                currentGoal = winterGoal;
                break;
            default:
                break;
        }
    }

    public void Harvest(Plant plant)
    {
        if (currentGoal.Contains(plant))
        {
            currentGoal.Remove(plant);
            if (currentGoal.Count == 0) 
            {
                switch (currentSeason)
                {
                    case SeasonEnum.SUMMER:
                        SetSeason(SeasonEnum.AUTOMN);
                        break;
                    case SeasonEnum.SPRING:
                        SetSeason(SeasonEnum.SUMMER);
                        break;
                    case SeasonEnum.AUTOMN:
                        SetSeason(SeasonEnum.WINTER);
                        break;
                    case SeasonEnum.WINTER:
                        SetSeason(SeasonEnum.SPRING);
                        break;
                    default:
                        break;

                }
            }
        }
    }
}
