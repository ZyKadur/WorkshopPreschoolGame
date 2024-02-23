using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Season : MonoBehaviour
{
    enum SeasonEnum
    {
        WINTER,
        SPRING,
        SUMMER,
        AUTOMN
    }

    [SerializeField] SeasonEnum startSeason = SeasonEnum.SPRING;

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

    [Header("Goal UI")]
    [SerializeField] private Image goalA;
    [SerializeField] private Image goalB;
    [SerializeField] private Image goalC;

    static public Season Instance { get; private set; }

    private SeasonEnum currentSeason;
    private List<Plant> currentGoal;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetSeason(startSeason);
    }

    private void SetSeason(SeasonEnum season)
    {
        currentSeason = season;
        
        switch (season)
        {
            case SeasonEnum.SUMMER:
                SetCurrentGoal(summerGoal);
                break;
            case SeasonEnum.SPRING:
                SetCurrentGoal(springGoal);
                break;
            case SeasonEnum.AUTOMN:
                SetCurrentGoal(automnGoal);
                break;
            case SeasonEnum.WINTER:
                SetCurrentGoal(winterGoal);
                break;
            default:
                break;
        }
    }

    private void SetCurrentGoal(List<Plant> goal)
    {
        currentGoal = goal;
        goalA.sprite = goal[0].productSprite;
        goalB.sprite = goal[1].productSprite;
        goalC.sprite = goal[2].productSprite;
        goalA.color = Color.white;
        goalB.color = Color.white;
        goalC.color = Color.white;
    }

    public void Harvest(Plant plant)
    {
        if (currentGoal.Contains(plant))
        {
            switch (currentGoal.IndexOf(plant))
            {
                case 0:
                    goalA.color = Color.gray;
                    break;
                case 1:
                    goalB.color = Color.gray;
                    break;
                case 2:
                    goalC.color = Color.gray;
                    break;
                default : 
                    break;
            }

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
