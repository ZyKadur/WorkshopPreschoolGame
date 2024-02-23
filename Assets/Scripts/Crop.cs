using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Crop : MonoBehaviour
{
    public enum CropState
    {
        EMPTY,
        HOLE,
        SEED,
        GROWING,
        HARVASTABLE
    }


    [HideInInspector] public CropState state = CropState.EMPTY;
    [HideInInspector] public Plant plant;

    [Header("Object in Prefab")]
    [SerializeField] private SpriteRenderer plantSprite;
    [SerializeField] private ParticleSystem transitionParticle;

    [Header("Sprites")]
    [SerializeField] private Sprite holeSprite;

    [Header("Debug")]
    [SerializeField] private Plant debugPlant;

    private float growTimer = 0;
    [SerializeField] private float timeToGrow = 2;

    private void Start()
    {
        state = CropState.EMPTY;
        plant = null;
    }

    private void Update()
    {
        if (state == CropState.GROWING)
        {
            growTimer += Time.deltaTime;

            if (growTimer >= timeToGrow) 
                Grow();
        }
    }

    private void OnMouseDown()
    {
        Harvest();
    }

    [ContextMenu("Dig")]
    public void Dig()
    {
        if (state != CropState.EMPTY)
            return;

        transitionParticle?.Play();
        state = CropState.HOLE;
        plantSprite.sprite = holeSprite;
    }

    [ContextMenu("PlantDebug")]
    public void PlantDebug()
    {
        Plant(debugPlant);
    }

    public void Plant(Plant _plant)
    {
        if (state != CropState.HOLE)
            return;

        transitionParticle?.Play();
        state = CropState.SEED;
        plant = _plant;
        plantSprite.sprite = plant?.seedSprite;
    }

    [ContextMenu("Water")]
    public void Water()
    {
        if (state != CropState.SEED)
            return;

        transitionParticle?.Play();
        state = CropState.GROWING;
        plantSprite.sprite = plant?.growingSprite;
    }

    [ContextMenu("Grow")]
    public void Grow()
    {
        if (state != CropState.GROWING)
            return;

        transitionParticle?.Play();
        growTimer = 0;
        state = CropState.HARVASTABLE;
        plantSprite.sprite = plant?.harvastableSprite;
    }

    [ContextMenu("Harvest")]
    public void Harvest()
    {
        if (state != CropState.HARVASTABLE)
            return;

        Season.Instance?.Harvest(plant);

        transitionParticle?.Play();
        state = CropState.EMPTY;
        plant = null;
        plantSprite.sprite = null;
    }
}
