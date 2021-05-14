using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class HungerInitialiser : BehaviourInitialiser
{
    public HungerInitialiser()
    {
        foreach (var act in ActionList.humanFoodActions)
        {
            actionList.Add(act);
        }

        requiredWorldStates = new GOAPWorldState();

        var worldState = requiredWorldStates;
        worldState.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        worldState.CreateElement(WorldValues.holdItemObject, null);

        worldState.CreateElement(WorldValues.hunger, 100.0f);
        worldState.CreateElement(WorldValues.hasProcessedHunger, false);

        updater = new HungerUpdate();
    }
}
