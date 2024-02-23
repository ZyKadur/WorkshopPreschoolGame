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

    public CropState state = CropState.EMPTY;
    public Plant plant;

    [SerializeField] private SpriteRenderer plantSprite;

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

    public void Dig()
    {
        if (state != CropState.EMPTY)
            return;

        state = CropState.HOLE;
        plantSprite.sprite = null;
    }

    public void Plant(Plant _plant)
    {
        if (state != CropState.HOLE)
            return;

        state = CropState.SEED;
        plant = _plant;
        plantSprite.sprite = plant.seedSprite;
    }

    public void Water()
    {
        if (state != CropState.SEED)
            return;

        state = CropState.GROWING;
        plantSprite.sprite = plant.growingSprite;
    }

    public void Grow()
    {
        if (state != CropState.GROWING)
            return;

        growTimer = 0;
        state = CropState.HARVASTABLE;
        plantSprite.sprite = plant.harvastableSprite;
    }

    public void Harvest()
    {
        if (state != CropState.HARVASTABLE)
            return;

        state = CropState.EMPTY;
        plant = null;
        plantSprite.sprite = null;
    }
}
