using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public static class BehaviourComponenets
{
    public static BehaviourInitialiser hungerComponent;
    public static BehaviourInitialiser woodCutterComponent;

    public static void Init()
    {
        InitHungerComponent();
        InitWoodCutter();
    }

    static void InitHungerComponent()
    {
        hungerComponent = new BehaviourInitialiser();

        foreach(var act in ActionList.humanFoodActions)
        {
            hungerComponent.actionList.Add(act);
        }

        hungerComponent.requiredWorldStates = new GOAPWorldState();

        var worldState = hungerComponent.requiredWorldStates;
        worldState.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        worldState.CreateElement(WorldValues.holdItemObject, null);

        worldState.CreateElement(WorldValues.hunger, 100.0f);
        worldState.CreateElement(WorldValues.hasProcessedHunger, false);

        hungerComponent.updater = new HungerUpdate();
    }

    static void InitWoodCutter()
    {
        woodCutterComponent = new BehaviourInitialiser();

        foreach(var act in ActionList.humanWoodActions)
        {
            hungerComponent.actionList.Add(act);
        }

        woodCutterComponent.requiredWorldStates = new GOAPWorldState();

        var worldState = woodCutterComponent.requiredWorldStates;
        worldState.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        worldState.CreateElement(WorldValues.holdItemObject, null);

        woodCutterComponent.updater = new WoodCutterUpdate();
    }
}
