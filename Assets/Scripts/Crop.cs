using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
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

    public void Dig()
    {
        if (state != CropState.EMPTY)
            return;

        state = CropState.HOLE;
    }

    public void Plant(Plant _plant)
    {
        if (state != CropState.HOLE)
            return;

        state = CropState.SEED;
        plant = _plant;
    }

    public void Water()
    {
        if (state != CropState.SEED)
            return;

        state = CropState.GROWING;
    }

    public void Grow()
    {
        if (state != CropState.GROWING)
            return;

        state = CropState.HARVASTABLE;
    }

    public void Harvest()
    {
        if (state != CropState.HARVASTABLE)
            return;

        state = CropState.EMPTY;
        plant = null;
    }
}
