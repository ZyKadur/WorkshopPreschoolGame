using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInteractionManager : MonoBehaviour
{
    public void DoInteraction(DragDropTool.ToolType toolType, Crop targetCrop, Plant plant = null)
    {
        Debug.Log(toolType);

        switch (toolType)
        {
            case DragDropTool.ToolType.Arrosoir:
                targetCrop.Water();
                break;
            case DragDropTool.ToolType.Seed:
                targetCrop.Plant(plant);
                break;
            case DragDropTool.ToolType.Shovel:
                targetCrop.Dig();
                break;
        }
    }
}
