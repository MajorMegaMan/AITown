using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : GOAPAction
{
    public EatFood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.food);

        preconditions.CreateElement(WorldValues.isHoldingItem, true);
        effects.CreateElement(WorldValues.isHoldingItem, false);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.hunger, 100.0f);
        effects.CreateElement(WorldValues.hasProcessedHunger, false);

        name = "Eat Food";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        state.SetElementValue(WorldValues.hunger, 100.0f);
        state.SetElementValue(WorldValues.hasProcessedHunger, false);
        state.CreateElement(WorldValues.isHoldingItem, false);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        AddEffects(worldState);

        // Get held wood object and destroy it
        var data = worldState.GetData(WorldValues.holdItemObject);
        GameObject foodObject = (GameObject)(data.value);

        //GameObject woodObject = worldState.GetElementValue<GameObject>(WorldValues.holdItemObject);
        GameObject.Destroy(foodObject);

        data.value = null;

        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return true;
    }
}
